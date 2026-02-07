using System;

namespace BuilderDesignPattern
{
    public class UserBuilder
    {
        private string? _name;
        private int? _age;
        private string? _email;
        private string? _phone;

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public UserBuilder WithAge(int age)
        {
            _age = age;
            return this;
        }
        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public UserBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public User Build()
        {
            if (string.IsNullOrWhiteSpace(_name))
                throw new InvalidOperationException("Name is required");

            return new User(_name, _age, _email, _phone);
        }
    }
}