using AuthorizeApi.Model;
using AuthorizeApi.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace AuthorizeApi.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));

       private IConfiguration _config;  
       private readonly IAuthenticateProvider authenticateProvider;
        public AuthController(IAuthenticateProvider _authenticateProvider, IConfiguration config)
        {
            _log4net.Info("AuthController called");
            authenticateProvider = _authenticateProvider;
            _config = config;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<String> getvalues()
        {
            
            return new String[] { "user1", "user2" };
        }
        [AllowAnonymous]
        [HttpPost, Route("createtoken")]
        
        public IActionResult CreateToken(Credentials login)
        {
            var userDetails = authenticateProvider.AuthenticateLogin(login);
            try
            {
                if(login.Equals(null))
                {
                    _log4net.Info("Cred login is Null");
                    return BadRequest(" Login cannot be null");
                }
                _log4net.Info("In AuthController CreateToken "+ login +"is found with..");

                IActionResult response = Unauthorized();
                if(userDetails==null)
                {
                    _log4net.Info(login + " is invalid Cred.");
                    return NotFound("Invalid login details");
                }
                else
                {
                    var tokenString = BuildToken(userDetails);
                    response = Ok(new { token = tokenString });
                }
                _log4net.Info("In AuthController CreateToken response "+response + " is working.");
                return response;
                
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Found = " + e.Message);
                return new StatusCodeResult(500);
            }
        }

        private string BuildToken(Agent user)
        {
            if (user == null)
            {
                _log4net.Info(user + " is invalid Credentials.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            _log4net.Info("key " + key + " has been created successfully ");
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            _log4net.Info("creds " + creds + " has been created successfully ");
            try
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Role, "Agent"),
                new Claim("UserName", user.Name.ToString())
                };
                
                    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(15),
                     signingCredentials: creds);

                    _log4net.Info("token " + token + "has been created successfully");
                    return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Found = " + e.Message);
                return e.Message;
            }

        }
        


    }
}
