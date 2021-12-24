using AuthorizeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeApi.Provider
{
    public interface IAuthenticateProvider
    {
        public Agent AuthenticateLogin(Credentials login);


    }
}
