// Application/Interfaces/IAuthService.cs
namespace Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(string email, string password);
        Task<string> Login(string email, string password);
    }
}
