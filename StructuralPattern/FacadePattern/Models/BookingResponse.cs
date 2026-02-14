namespace FacadePattern.Models
{
    public class BookingResponse
    {
        public bool Success { get; }
        public string ConfirmationNumber { get; }
        public string Message { get; }

        public BookingResponse(bool success, string confirmationNumber, string message)
        {
            Success = success;
            ConfirmationNumber = confirmationNumber;
            Message = message;
        }

        public static BookingResponse Ok(string confirmationNumber)
        {
            return new BookingResponse(true, confirmationNumber, "Booked successfully.");
        }

        public static BookingResponse Failed(string message)
        {
            return new BookingResponse(false, "", message);
        }
    }
}
