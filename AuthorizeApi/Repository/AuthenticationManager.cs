using AuthorizeApi.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeApi.Repository
{
    public class AuthenticationManager:IAuthenticationManager
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthenticationManager));

        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        { {"user1", "user1" }, {"user2", "user2" } };
        
      
        private readonly string key;

        public AuthenticationManager(string key)
        {
            this.key = key;
        }
       
        private static List<Credentials> List = new List<Credentials>()
        {
            new Credentials{ Email = "aman@gmail.com", Password = "aman"}
        };
        public List<Credentials> GetUserList()
        {
            return List;
        }
        public AuthenticationManager()
        {
            _log4net.Info("Authentication Manager Constructor created");
        }
        public Agent AuthenticateLogin(Credentials login) 
        {
            _log4net.Info("AuthenticationManager Authenticatelogin method is called ");
            if(login==null)
            {
                _log4net.Error("Login cannot be null in AuthenticateLogin Method : AuthenticationManager");
            }
            try
            {
                if (login != null)
                {
                    List<Credentials> userList = GetUserList();
                    Agent user = null;
                    Credentials penCred = userList.FirstOrDefault(user => user.Email == login.Email && user.Password == login.Password);
                    user = new Agent { Name = "Aman Chaudhary", Email = "aman@domain.com" };
                    _log4net.Info("Agent user is created in AuthenticateLogin method ");
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                _log4net.Error("Exception encountered while creating email and password" + e.Message);
                return null;
            }
        }
        public string Authenticate(string username, string password)
          {
            _log4net.Info("In AuthenticationManager Authenticate method is called ");
            try
            {
                if (!users.Any(u => u.Key == username && u.Value == password))
                {
                    return null;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                _log4net.Info("Token Handler " + tokenHandler + "is created");
                var tokenKey = Encoding.ASCII.GetBytes(key);
                _log4net.Info("Token Key " + tokenKey + "is created");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.Now.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception e)
            {
                _log4net.Info("Authenticate Method is giving error" + e.Message);
                return null;
            }
        }
    }
}
