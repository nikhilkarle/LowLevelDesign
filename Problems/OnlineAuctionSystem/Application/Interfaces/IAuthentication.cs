using OAS.Domain.Entities;

namespace OAS.Application.Interfaces;

public interface IAuthenticationService
{
    User Register(string userName, string email, string password);
    User Login(string email, string password);
}