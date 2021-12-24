using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeApi.Model;
using AuthorizeApi.Repository;
using Microsoft.Extensions.Configuration;

namespace AuthorizeApi.Provider
{
    public class AuthenticateProvider : IAuthenticateProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthenticateProvider));

        private readonly IAuthenticationManager _authenticationManager;
        public AuthenticateProvider(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
       
        public Agent AuthenticateLogin(Credentials login)
        {
            if (login == null)
            {
                _log4net.Error("Login cannot be null in AuthenticateLogin Method : AuthenticationManager");
            }
            Agent user1;
            try
            {
                _log4net.Info("AuthController has Called AuthenticateLogin and " + login + " is searched in AuthenticateProvider");
                user1 = _authenticationManager.AuthenticateLogin(login);
            }
            catch (Exception e)
            {
                _log4net.Error("In AuthenticateProvider encountered exception" + e.Message);
                return null;
            }
            return user1;


        }
    }
}
