using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Factories;
using RestaurantManagementSystem.Application.Observers;
using RestaurantManagementSystem.Application.Services;
using RestaurantManagementSystem.Application.Strategies;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Infrastructure.Payments;
using RestaurantManagementSystem.Infrastructure.Repositories;

var menuItemRepo    = new InMemoryMenuItemRepository();
var orderRepo       = new InMemoryOrderRepository();
var reservationRepo = new InMemoryReservationRepository();
var customerRepo    = new InMemoryCustomerRepository();
var staffRepo       = new InMemoryStaffRepository();
var inventoryRepo   = new InMemoryInventoryRepository();
var invoiceRepo     = new InMemoryInvoiceRepository();
var paymentRepo     = new InMemoryPaymentRepository();
var tableRepo       = new InMemoryTableRepository();

var paymentProcessors = new Dictionary<PaymentMethod, RestaurantManagementSystem.Application.Interfaces.IPaymentProcessor>
{
    { PaymentMethod.Cash,          new CashPaymentProcessor() },
    { PaymentMethod.CreditCard,    new CreditCardPaymentProcessor() },
    { PaymentMethod.MobilePayment, new MobilePaymentProcessor() }
};

var menuService        = new MenuService(menuItemRepo);
var inventoryService   = new InventoryService(inventoryRepo);
var billingService     = new BillingService(invoiceRepo, paymentRepo, paymentProcessors);
var orderService       = new OrderService(orderRepo, menuItemRepo, inventoryService, billingService, new OrderFactory());
var reservationService = new ReservationService(reservationRepo, customerRepo, tableRepo);
var staffService       = new StaffService(staffRepo);
var customerService    = new CustomerService(customerRepo);
var reportService      = new ReportService(orderRepo, inventoryRepo, paymentRepo, new SalesReportStrategy());

orderService.Subscribe(new KitchenDisplayObserver());
orderService.Subscribe(new CustomerNotificationObserver());

Console.WriteLine("=== Seed Inventory ===");
var flourId  = inventoryService.AddItem("Flour",   5000, "g",  500).Id;
var cheeseId = inventoryService.AddItem("Cheese",  2000, "g",  300).Id;
var tomatoId = inventoryService.AddItem("Tomato",  3000, "g",  400).Id;
var chickenId= inventoryService.AddItem("Chicken", 4000, "g",  500).Id;
var milkId   = inventoryService.AddItem("Milk",    3000, "ml", 400).Id;
Console.WriteLine("Inventory seeded.");

Console.WriteLine("\n=== Seed Tables ===");
tableRepo.Add(new Table(Guid.NewGuid(), 1, 2));
tableRepo.Add(new Table(Guid.NewGuid(), 2, 4));
tableRepo.Add(new Table(Guid.NewGuid(), 3, 6));
Console.WriteLine("Tables 1 (2-top), 2 (4-top), 3 (6-top) added.");

Console.WriteLine("\n=== Build Menu ===");
var margherita = menuService.AddItem(
    "Margherita Pizza", "Classic tomato & cheese", 14.99m, MenuCategory.MainCourse,
    new() { [flourId] = 300, [tomatoId] = 200, [cheeseId] = 150 });

var pasta = menuService.AddItem(
    "Creamy Pasta", "Pasta in a rich cream sauce", 12.99m, MenuCategory.MainCourse,
    new() { [flourId] = 200, [milkId] = 150 });

var chickenBurger = menuService.AddItem(
    "Chicken Burger", "Grilled chicken with house sauce", 11.99m, MenuCategory.MainCourse,
    new() { [chickenId] = 250, [flourId] = 100 });

var iceCream = menuService.AddItem(
    "Ice Cream", "Two scoops of vanilla", 5.99m, MenuCategory.Dessert,
    new() { [milkId] = 100 });

Console.WriteLine($"Menu items added: {margherita.Name}, {pasta.Name}, {chickenBurger.Name}, {iceCream.Name}");

Console.WriteLine("\n=== Register Customers ===");
var alice = customerService.Register("Alice",   "alice@email.com",   "+1-555-0101");
var bob   = customerService.Register("Bob",     "bob@email.com",     "+1-555-0102");
var carol = customerService.Register("Carol",   "carol@email.com",   "+1-555-0103");
Console.WriteLine($"Registered: {alice.Name}, {bob.Name}, {carol.Name}");

Console.WriteLine("\n=== Staff Management ===");
var chef    = staffService.AddStaff("Marco",   "marco@resto.com",   StaffRole.Chef);
var waiter  = staffService.AddStaff("Sara",    "sara@resto.com",    StaffRole.Waiter);
var manager = staffService.AddStaff("James",   "james@resto.com",   StaffRole.Manager);

staffService.AddShift(chef.Id, DateTime.Today.AddHours(9), DateTime.Today.AddHours(17));
staffService.AddShift(waiter.Id, DateTime.Today.AddHours(11), DateTime.Today.AddHours(22));

staffService.UpdatePerformanceScore(chef.Id, 96.5);
Console.WriteLine($"Staff: {chef.Name} (Chef, score={chef.PerformanceScore}), {waiter.Name} (Waiter), {manager.Name} (Manager)");

Console.WriteLine("\n=== Reservations ===");
var resReq = new MakeReservationRequest
{
    CustomerId = alice.Id,
    PartySize  = 3,
    Date       = DateTime.Today,
    TimeSlot   = TimeSpan.FromHours(19)   // 7 PM
};

var reservation = reservationService.MakeReservation(resReq);
reservationService.ConfirmReservation(reservation.Id);
Console.WriteLine($"Reservation status: {reservation.Status}, Table: {reservation.TableId}");

Console.WriteLine("\n=== Place Order (Alice) ===");
var aliceOrder = orderService.PlaceOrder(new PlaceOrderRequest
{
    CustomerId = alice.Id,
    TableId    = reservation.TableId!.Value,
    Items =
    [
        new() { MenuItemId = margherita.Id, Quantity = 2 },
        new() { MenuItemId = iceCream.Id,   Quantity = 1, SpecialInstructions = "Extra chocolate sauce" }
    ]
});
Console.WriteLine($"Order {aliceOrder.Id} placed. Total: ${aliceOrder.TotalAmount:F2} | Status: {aliceOrder.Status}");

Console.WriteLine("\n=== Order Lifecycle ===");
orderService.StartPreparing(aliceOrder.Id);
orderService.MarkReady(aliceOrder.Id);
orderService.MarkServed(aliceOrder.Id);
Console.WriteLine($"Final order status: {aliceOrder.Status}");

Console.WriteLine("\n=== Billing & Payment ===");
var invoice = billingService.GetInvoiceByOrder(aliceOrder.Id);
Console.WriteLine($"Invoice #{invoice.Id} | Total: ${invoice.TotalAmount:F2}");
foreach (var line in invoice.Lines)
    Console.WriteLine($"  {line.Description,-30} ${line.Amount:F2}");

var payment = billingService.ProcessPayment(new PaymentRequest
{
    InvoiceId = invoice.Id,
    Method    = PaymentMethod.CreditCard
});
Console.WriteLine($"Payment status: {payment.Status}");

Console.WriteLine("\n=== Place & Cancel Order (Bob) ===");
var bobOrder = orderService.PlaceOrder(new PlaceOrderRequest
{
    CustomerId = bob.Id,
    TableId    = tableRepo.GetAvailable(2).First().Id,
    Items = [ new() { MenuItemId = chickenBurger.Id, Quantity = 1 } ]
});
Console.WriteLine($"Bob's order {bobOrder.Id} placed. Status: {bobOrder.Status}");
orderService.CancelOrder(bobOrder.Id, menuService);
Console.WriteLine($"Bob's order cancelled. Status: {bobOrder.Status}");

Console.WriteLine("\n=== State Guard: Invalid Transition ===");
try
{
    orderService.StartPreparing(aliceOrder.Id);   
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Caught: {ex.Message}");
}

Console.WriteLine("\n=== Mobile Payment (Carol's order) ===");
var carolOrder = orderService.PlaceOrder(new PlaceOrderRequest
{
    CustomerId = carol.Id,
    TableId    = tableRepo.GetAvailable(2).First().Id,
    Items = [ new() { MenuItemId = pasta.Id, Quantity = 2 } ]
});
orderService.StartPreparing(carolOrder.Id);
orderService.MarkReady(carolOrder.Id);
orderService.MarkServed(carolOrder.Id);

var carolInvoice = billingService.GetInvoiceByOrder(carolOrder.Id);
billingService.ProcessPayment(new PaymentRequest { InvoiceId = carolInvoice.Id, Method = PaymentMethod.MobilePayment });

Console.WriteLine("\n=== Sales Report ===");
reportService.SetStrategy(new SalesReportStrategy());
var salesReport = reportService.Generate(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));
Console.WriteLine($"[{salesReport.Title}]");
PrintReport(salesReport.Data);

Console.WriteLine("\n=== Inventory Report ===");
reportService.SetStrategy(new InventoryReportStrategy());
var inventoryReport = reportService.Generate(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));
Console.WriteLine($"[{inventoryReport.Title}]");
PrintReport(inventoryReport.Data);

static void PrintReport(Dictionary<string, object> data)
{
    foreach (var kv in data)
    {
        if (kv.Value is System.Collections.IEnumerable enumerable and not string)
        {
            Console.WriteLine($"  {kv.Key}:");
            foreach (var item in enumerable)
                Console.WriteLine($"    - {item}");
        }
        else
        {
            Console.WriteLine($"  {kv.Key}: {kv.Value}");
        }
    }
}

Console.WriteLine("\n=== Complete Reservation ===");
reservationService.CompleteReservation(reservation.Id);
Console.WriteLine($"Reservation status: {reservation.Status}");