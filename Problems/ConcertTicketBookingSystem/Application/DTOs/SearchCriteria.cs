namespace ConcertTicketBookingSystem.Application.DTOs;

public class SearchCriteria
{
    public string?   Artist            { get; set; }
    public Guid?     VenueId           { get; set; }
    public DateTime? FromDate          { get; set; }
    public DateTime? ToDate            { get; set; }
    public decimal?  MaxPrice          { get; set; }
    public int?      MinAvailableSeats { get; set; }
}
