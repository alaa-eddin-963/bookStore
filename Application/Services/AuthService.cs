using Shared.Utils;
using Data;
using Models;
using Interfaces;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthService(AppDbContext context, JwtTokenHelper jwtTokenHelper)
        {
            _context = context;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<string> Register(string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
                throw new BadHttpRequestException("User already exists");

            var hashed = PasswordHelper.HashPassword(password);
            var user = new User { Email = email, PasswordHash = hashed };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _jwtTokenHelper.GenerateToken(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email)
                ?? throw new UnauthorizedAccessException("Invalid credentials");

            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return _jwtTokenHelper.GenerateToken(user);
        }
    }
}
