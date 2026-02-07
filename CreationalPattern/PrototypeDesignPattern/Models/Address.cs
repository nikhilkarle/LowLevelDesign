namespace PrototypeDesignPattern.Models
{
    public class Address
    {
        public string Street {get;}
        public string City {get;} 

        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public Address Clone()
        {
            return new Address(Street, City);
        }

        public override string ToString()
        {
            return $"{Street}, {City}";
        }
    }

}