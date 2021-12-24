using AuthorizeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeApi.Repository
{
    public interface IAuthenticationManager
    {

        public List<Credentials> GetUserList();
        string Authenticate(string username, string password);
        public Agent AuthenticateLogin(Credentials login);
    }
}
