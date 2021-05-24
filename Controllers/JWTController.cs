using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using testrm.Data;
using testrm.Models;

namespace testrm.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : Controller
    {
        private IConfiguration _config { get; }
        private readonly DB_BackOfficeContext _ent;
        public JWTController(IConfiguration config, DB_BackOfficeContext ent)
        {
            _ent = ent;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]User login)
        {
            IActionResult respone = Unauthorized();//401
            var user = Authenticate(login);
            if (user != null)
            {
                var tokenString = BuildToken(user);
                respone = Ok(new { token = tokenString });
            }
            return respone;
        }

        private User Authenticate(User login)
        {
            User user = _ent.User.FirstOrDefault(
                x => x.user.Equals(login.user) && x.pass.Equals(login.pass));
            return user;
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config["JWT:Expires"]));
            //PayLoad
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.name),
                new Claim(JwtRegisteredClaimNames.Email, user.mail),
                new Claim("Auth Test","JWT"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                _config["JWT:Issuer"],
                _config["JWT:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}