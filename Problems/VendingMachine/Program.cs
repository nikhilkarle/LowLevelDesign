using VendingMachine.Domain;
using VendingMachine.Events;
using VendingMachine.Repositories;
using VendingMachine.Strategies;
using VendingMachine.Concurrency;
using VendingMachine.Facade;

var inventory = new InMemoryInventoryRepository(new[]
{
    new Product("A1", "Chips", 150, 5),
    new Product("B2", "Soda", 225, 2),
    new Product("C3", "Candy", 100, 0),
});

var cash = new InMemoryCashRepository();
cash.Restock(new Dictionary<Denomination,int>
{
    [Denomination.Cent25] = 20,
    [Denomination.Cent10] = 30,
    [Denomination.Cent5]  = 40,
    [Denomination.Cent1]  = 100,
});

var events = new InMemoryEventPublisher();
events.Subscribe(e => Console.WriteLine($"[EVENT] {e}"));

var locks = new LockProvider();
var changeStrategy = new GreedyBoundedChangeStrategy();

var vm = new VendingMachineClass(inventory, cash, changeStrategy, events, locks);

var tx = vm.StartTransaction();
vm.SelectProduct(tx, "B2");
vm.Insert(tx, Denomination.Dollar1);
vm.Insert(tx, Denomination.Dollar1);
vm.Insert(tx, Denomination.Dollar1);
var result = vm.Complete(tx);

Console.WriteLine(result);

vm.AdminRestockProduct("C3", addedQuantity: 10);
Console.WriteLine("Restocked C3.");
