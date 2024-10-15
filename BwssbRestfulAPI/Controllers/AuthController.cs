using BwssbRestfulAPI.IRepository;
using BwssbRestfulAPI.Models.JWToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BwssbRestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommonRepository _commonRepository;

        public AuthController(IConfiguration configuration,ICommonRepository commonRepository)
        {
            _configuration = configuration;
            _commonRepository = commonRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RequestToken loginModel)
        {
            string BwssbpKey = _configuration["Appsettings:BwssBKey"];
            int CCID = 0;

            if (!string.IsNullOrEmpty(BwssbpKey) && BwssbpKey == loginModel.BwssBKey) 
            {

                if (!string.IsNullOrEmpty(loginModel.CCID)) 
                {
                    CCID = int.Parse(loginModel.CCID);

                    var (data, message)  = await _commonRepository.GetCashCounterByCCIDAsync(CCID);

                    if (string.IsNullOrEmpty(message))
                    {
                        return NotFound(new { message = "No record found for the provided CCID." });
                    }
                    else
                    {
                        var token = GenerateJwtToken(loginModel.CCID);
                        return Ok(new { token });
                    }

                }
                else
                {
                    return NotFound(new { message = "CCID is Not Found" });
                }
            
            }
            else
            {
                return NotFound(new { message = "BwssbKey is missing" });
                
            }

            //return Unauthorized("Invalid credentials");
        }


        // Generate JWT Token
        private string GenerateJwtToken(string CCID)
        {

            var jwtSecretKey = _configuration["JWT:SecretKey"];
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];

            var claims = new[]
            {
                new Claim("CCID", CCID),                
                new Claim(JwtRegisteredClaimNames.Sub, "BWSSB"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

