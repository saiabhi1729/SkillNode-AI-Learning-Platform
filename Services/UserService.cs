using Microsoft.EntityFrameworkCore;
using SkillNode_Backend.Data;
using SkillNode_Backend.Models;
using SkillNode_Backend.Models.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace SkillNode_Backend.Services
{
    public interface IUserService
    {
        Task<bool> UserExists(string email);
        Task<User?> RegisterUserAsync(RegisterUserDto request);
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<User?> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
    }

    public class UserService : IUserService
    {
        private readonly SkillNodeDbContext _context;

        public UserService(SkillNodeDbContext context)
        {
            _context = context;
        }

        //Check if a User Already Exists
        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        //Register a New User (Save to DB)
        public async Task<User?> RegisterUserAsync(RegisterUserDto request)
        {
            if (await UserExists(request.Email)) return null; // Prevent duplicates

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password), // Hash the password
                Role = request.Role?? "Student",
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        //Authenticate User (Verify Email & Password)
        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !VerifyPassword(password, user.PasswordHash)) return null; // Invalid Credentials
            return user;
        }

        //Get User By ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        //Get All Users (Admin Only)
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //Hash Password (Security)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        //Verify Password (Compare Hashes)
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
