using Secim2028.Dtos.UserDto;

namespace Secim2028.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(UserDto request);
        Task<string> Login(UserDto request);

        string CreateToken(User user);

    }
}
