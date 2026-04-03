namespace LibraryManagementSystem.Domain.ValueObjects
{
    public class ContactInfo
    {
        public string Email { get; }
        public string Phone { get; }
        public string Address { get; }

        public ContactInfo(string email, string phone, string address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }
    }
}