namespace BuilderDesignPattern
{
    public class User
    {
        public string Name {get; set;}
        public int? Age {get;set;}
        public string? Email{get;set;}
        public string? Phone{get;set;}

        public User(string name, int? age, string? email, string? phone)
        {
            Name = name;
            Age = age;
            Email = email;
            Phone = phone;
        }

        public override string ToString()
        => $"Name: {Name}, $Age: {Age}, $Email: {Email}, $Phone: {Phone}";
    }
}