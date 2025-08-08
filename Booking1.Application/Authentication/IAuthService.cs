namespace Booking1.Application.Authentication
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginDto loginDto);
    }
}
