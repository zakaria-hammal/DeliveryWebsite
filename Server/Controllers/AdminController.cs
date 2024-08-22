using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        public IConfiguration config;
        public AdminController(IConfiguration configuration)
        {
            config = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginUserModel loginUserModel)
        {
            if(loginUserModel == null)
                return BadRequest();

            var json = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "Data/AdminInfos.json"));
            var Admin = JsonSerializer.Deserialize<LoginUserModel>(json);
            if(Admin == null || Admin.Username == null|| config["Jwt:Key"] == null)
                return StatusCode(500);

            if(Admin.Username != loginUserModel.Username || Admin.Password != loginUserModel.Password)
                return NotFound("Username or/and Password not found");

            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.GivenName, Admin.Username),
                                new Claim(ClaimTypes.Role, "Admin")
                            };

            var key = config["Jwt:Key"];

            if(key == null)
                return StatusCode(500);

            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = creds,
                    Issuer = config["Jwt:Issuer"],
                    Audience = config["Jwt:Audience"]
                };
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(tokenHandler.WriteToken(token));
        }

        [HttpPost]
        public IActionResult ResetPassword([FromBody] ChangeModel changeModel)
        {
            if(changeModel == null)
                return BadRequest();

            var text = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "Data/AdminInfos.json"));
            var Ad = JsonSerializer.Deserialize<LoginUserModel>(text);

            if(changeModel.CurrentPassword != Ad.Password)
            {
                return BadRequest();
            }

            var Admin = new LoginUserModel
                            {
                                Username = "Admin",
                                Password = changeModel.NewPassword
                            };

            var json = JsonSerializer.Serialize(Admin, new JsonSerializerOptions{WriteIndented = true});
            System.IO.File.WriteAllText(Path.Combine(System.Environment.CurrentDirectory, "Data/AdminInfos.json"), json);

            return Ok("Password Changed Successfully");
        }        
    }
}