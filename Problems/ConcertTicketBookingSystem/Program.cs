using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Factories;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Application.Observers;
using ConcertTicketBookingSystem.Application.Services;
using ConcertTicketBookingSystem.Application.Specifications.Concerts;
using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;
using ConcertTicketBookingSystem.Infrastructure.BackgroundServices;
using ConcertTicketBookingSystem.Infrastructure.Notifications;
using ConcertTicketBookingSystem.Infrastructure.Payments;
using ConcertTicketBookingSystem.Infrastructure.Repositories;

var venueRepo        = new InMemoryVenueRepository();
var concertSeatRepo  = new InMemoryConcertSeatRepository();

var expiryService = new ReservationExpiryService(concertSeatRepo, interval: TimeSpan.FromMinutes(1));
expiryService.Start();
var concertRepo     = new InMemoryConcertRepository();
var bookingRepo     = new InMemoryBookingRepository();
var userRepo        = new InMemoryUserRepository();
var waitingListRepo = new InMemoryWaitingListRepository();

var processors = new Dictionary<PaymentMethod, IPaymentProcessor>
{
    { PaymentMethod.CreditCard, new CreditCardProcessor() },
    { PaymentMethod.PayPal,     new PayPalProcessor()     },
    { PaymentMethod.UPI,        new UpiProcessor()        }
};

var emailSender = new ConsoleEmailSender();
var smsSender   = new ConsoleSMSSender();
var notifFactory = new NotificationFactory(emailSender, smsSender);

var paymentService      = new PaymentService(processors);
var bookingService      = new BookingService(concertSeatRepo, bookingRepo, paymentService);
var searchService       = new ConcertSearchService(concertRepo, concertSeatRepo, venueRepo);
var waitingListService  = new WaitingListService(waitingListRepo);

bookingService.Subscribe(new EmailNotificationHandler(userRepo, emailSender));
bookingService.Subscribe(new SmsNotificationHandler(userRepo, smsSender));
bookingService.Subscribe(new WaitingListHandler(waitingListRepo, userRepo, emailSender));

Console.WriteLine("=== Seed Venue ===");
var venue = new Venue(Guid.NewGuid(), "Madison Square Garden", "4 Pennsylvania Plaza, NYC", 500);
var vs1 = venue.AddSeat("VIP",     "A", 1, SeatType.VIP);
var vs2 = venue.AddSeat("VIP",     "A", 2, SeatType.VIP);
var vs3 = venue.AddSeat("General", "B", 1, SeatType.Regular);
var vs4 = venue.AddSeat("General", "B", 2, SeatType.Regular);
venueRepo.Add(venue);
Console.WriteLine($"Venue '{venue.Name}' created with {venue.Seats.Count} physical seats.");

Console.WriteLine("\n=== Seed Concerts ===");
var taylorConcert = new Concert(Guid.NewGuid(), "Taylor Swift",  venue.Id, DateTime.Now.AddDays(30));
var coldplayConcert = new Concert(Guid.NewGuid(), "Coldplay",    venue.Id, DateTime.Now.AddDays(60));
concertRepo.Add(taylorConcert);
concertRepo.Add(coldplayConcert);
Console.WriteLine($"Added: {taylorConcert.ArtistName} ({taylorConcert.DateTime:yyyy-MM-dd})");
Console.WriteLine($"Added: {coldplayConcert.ArtistName} ({coldplayConcert.DateTime:yyyy-MM-dd})");

var cs1 = new ConcertSeat(Guid.NewGuid(), taylorConcert.Id, vs1.Id, 350.00m);
var cs2 = new ConcertSeat(Guid.NewGuid(), taylorConcert.Id, vs2.Id, 350.00m);
var cs3 = new ConcertSeat(Guid.NewGuid(), taylorConcert.Id, vs3.Id,  90.00m);
var cs4 = new ConcertSeat(Guid.NewGuid(), taylorConcert.Id, vs4.Id,  90.00m);
concertSeatRepo.Add(cs1); concertSeatRepo.Add(cs2);
concertSeatRepo.Add(cs3); concertSeatRepo.Add(cs4);
Console.WriteLine($"Taylor Swift concert seeded with {4} concert seats.");

var alice = new User(Guid.NewGuid(), "Alice", "alice@email.com", "+1-555-0101", NotificationChannel.Email);
var bob   = new User(Guid.NewGuid(), "Bob",   "bob@email.com",   "+1-555-0102", NotificationChannel.SMS);
var carol = new User(Guid.NewGuid(), "Carol", "carol@email.com", "+1-555-0103", NotificationChannel.Email);
userRepo.Add(alice); userRepo.Add(bob); userRepo.Add(carol);
Console.WriteLine($"\nRegistered users: {alice.Name} (Email), {bob.Name} (SMS), {carol.Name} (Email)");

Console.WriteLine("\n=== Search via SearchCriteria DTO (artist='taylor', min 2 seats) ===");
var criteria = new SearchCriteria { Artist = "taylor", MinAvailableSeats = 2 };
foreach (var c in searchService.Search(criteria))
{
    var v = searchService.GetVenue(c.VenueId)!;
    Console.WriteLine($"  {c.ArtistName} @ {v.Name} on {c.DateTime:yyyy-MM-dd}");
}

Console.WriteLine("\n=== Search via spec: affordable (≤$100) OR Coldplay ===");
var affordableOrColdplay = new MaxPriceSpecification(100m, concertSeatRepo)
    .Or(new ArtistSpecification("Coldplay"));

foreach (var c in searchService.Search(affordableOrColdplay))
{
    var v = searchService.GetVenue(c.VenueId)!;
    Console.WriteLine($"  {c.ArtistName} @ {v.Name}");
}

Console.WriteLine("\n=== Search via spec: Not Coldplay AND within next 45 days ===");
var notColdplaySoon = new ArtistSpecification("Coldplay").Not()
    .And(new DateRangeSpecification(DateTime.Now, DateTime.Now.AddDays(45)));

foreach (var c in searchService.Search(notColdplaySoon))
{
    var v = searchService.GetVenue(c.VenueId)!;
    Console.WriteLine($"  {c.ArtistName} @ {v.Name} on {c.DateTime:yyyy-MM-dd}");
}

Console.WriteLine("\n=== Alice books 2 VIP seats (CreditCard) ===");
var aliceBooking = bookingService.Book(new BookingRequest
{
    UserId        = alice.Id,
    ConcertId     = taylorConcert.Id,
    SeatIds       = [cs1.Id, cs2.Id],
    PaymentMethod = PaymentMethod.CreditCard
});
Console.WriteLine($"  Booking {aliceBooking.Id} | Status: {aliceBooking.Status} | Total: ${aliceBooking.TotalAmount:F2}");
Console.WriteLine($"  Seat statuses: {cs1.Status}, {cs2.Status}");

Console.WriteLine("\n=== Bob tries to book Alice's VIP seat (should fail) ===");
try
{
    bookingService.Book(new BookingRequest
    {
        UserId        = bob.Id,
        ConcertId     = taylorConcert.Id,
        SeatIds       = [cs1.Id],
        PaymentMethod = PaymentMethod.PayPal
    });
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Caught expected error: {ex.Message}");
}

Console.WriteLine("\n=== Bob books 2 General seats (UPI) ===");
var bobBooking = bookingService.Book(new BookingRequest
{
    UserId        = bob.Id,
    ConcertId     = taylorConcert.Id,
    SeatIds       = [cs3.Id, cs4.Id],
    PaymentMethod = PaymentMethod.UPI
});
Console.WriteLine($"  Booking {bobBooking.Id} | Status: {bobBooking.Status} | Total: ${bobBooking.TotalAmount:F2}");

Console.WriteLine("\n=== Concert sold out — Carol joins waiting list ===");
var availableNow = searchService.GetAvailableSeats(taylorConcert.Id).ToList();
Console.WriteLine($"  Available seats: {availableNow.Count}");

var waitEntry = waitingListService.Join(taylorConcert.Id, carol.Id, requestedSeatCount: 2);
Console.WriteLine($"  Carol's waiting list position: #{waitEntry.Position}");

Console.WriteLine("\n=== Failed payment — seats must roll back to Available ===");

var failingProcessors = new Dictionary<PaymentMethod, IPaymentProcessor>
{
    { PaymentMethod.CreditCard, new FailingPaymentProcessor() }
};
var failingPaymentService = new PaymentService(failingProcessors);
var failingBookingService  = new BookingService(concertSeatRepo, bookingRepo, failingPaymentService);

var cpSeat1 = new ConcertSeat(Guid.NewGuid(), coldplayConcert.Id, vs1.Id, 200.00m);
var cpSeat2 = new ConcertSeat(Guid.NewGuid(), coldplayConcert.Id, vs2.Id, 200.00m);
concertSeatRepo.Add(cpSeat1); concertSeatRepo.Add(cpSeat2);

Console.WriteLine($"  Coldplay cpSeat1 before: {cpSeat1.Status}");
try
{
    failingBookingService.Book(new BookingRequest
    {
        UserId        = alice.Id,
        ConcertId     = coldplayConcert.Id,
        SeatIds       = [cpSeat1.Id],
        PaymentMethod = PaymentMethod.CreditCard
    });
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Caught: {ex.Message}");
}
Console.WriteLine($"  Coldplay cpSeat1 after rollback: {cpSeat1.Status}   ← back to Available");

Console.WriteLine("\n=== Alice cancels her booking ===");
bookingService.CancelBooking(aliceBooking.Id);
Console.WriteLine($"  Alice's booking status: {aliceBooking.Status}");
Console.WriteLine($"  VIP seat statuses after cancel: {cs1.Status}, {cs2.Status}  ← Available again");

expiryService.Stop();
Console.WriteLine("\n=== Done ===");
