using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.Entities;

public class User
{
    public Guid                Id                 { get; }
    public string              Name               { get; }
    public string              Email              { get; }
    public string              Phone              { get; }
    public NotificationChannel PreferredChannel   { get; }

    public User(Guid id, string name, string email, string phone,
                NotificationChannel preferredChannel = NotificationChannel.Email)
    {
        Id = id; Name = name; Email = email; Phone = phone; PreferredChannel = preferredChannel;
    }
}
