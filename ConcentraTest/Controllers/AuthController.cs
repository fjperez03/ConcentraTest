using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ConcentraTest.Models;
using AutoMapper;
using ConcentraTest.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ConcentraTest.Controllers    
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOUser args)
        {
            bool response = await getUserData(args);

            if (response)
            {
                //var pp = genKey();
                var secretKey = _configuration["JwtSettings:SecretKey"];
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, args.userName),
                        // Agrega cualquier otro reclamo que desees incluir
                    }),
                    Expires = DateTime.UtcNow.AddHours(1), // Define la duración del token
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            else 
            {
                return BadRequest("Usuario y/o contraseña invalidos.");
            }
        }

        private async Task<bool> getUserData(DTOUser args)
        {
            bool response = false;

            try
            {
                using (var context = new ConcentraContext())
                {
                    response = await context.Users.AnyAsync(u => u.UserName == args.userName && u.Password == args.password);
                }
            }
            catch (Exception ex)
            {

            }
            
            return response;
        }
    }
}
