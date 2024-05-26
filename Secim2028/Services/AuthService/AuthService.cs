using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Secim2028.Dtos.UserDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Secim2028.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthService(DataContext context,IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            
        }



        public async Task<User> Register(UserDto request)
        {
            User user = new User();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);

            user.username = request.username;
            user.hashPassword = hashedPassword;
            user.role = "pleb";

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.username),
                new Claim(ClaimTypes.Role,user.role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("jwt:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        public async Task<string> Login(UserDto request)
        {
            var user = await _context.User.Where(e => e.username == request.username).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }
            

            if(!BCrypt.Net.BCrypt.Verify(request.password, user.hashPassword))
            {
                return null;
            }

            string token = CreateToken(user);

            return token;


        }
    }
}
