using NUnit.Framework;
using Moq;
using AuthorizeApi.Controllers;
using AuthorizeApi.Repository;
using AuthorizeApi.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Castle.Core.Configuration;
using AuthorizeApi.Provider;
using System;

namespace AuthorizeAPITest
{
    [TestFixture]
    public class Tests
    {
        //private Agent agents;
        List<Credentials> user = new List<Credentials>();

        [SetUp]
        public void Setup()
        {
            user = new List<Credentials>()
            {
                new Credentials{Email = "aman@gmail.com",Password = "aman"},
                new Credentials{Email = "preethi@gmail.com",Password = "preethi"}

            };

        }
        [TestCase("aman@gmail.com", "aman")]
        public void AuthenticateLogin_Returns_Object(string username, string password)
        {
            Mock<IAuthenticationManager> mock = new Mock<IAuthenticationManager>();
            mock.Setup(p => p.GetUserList()).Returns(user);
            AuthenticationManager pro = new AuthenticationManager();
            Credentials cred = new Credentials { Email = username, Password = password };

            var penCred = pro.AuthenticateLogin(cred);

            Assert.IsNotNull(penCred);
        }
        [TestCase("preethi@gmail.com","preethi")]
        public void CreateToken_Returns_Object(string username,string password)
        {
            
                Mock<IAuthenticationManager> mock = new Mock<IAuthenticationManager>();
                mock.Setup(p => p.GetUserList()).Returns(user);
                AuthenticationManager pro = new AuthenticationManager();
                Credentials cred = new Credentials { Email = username, Password = password };
                var penCred = pro.AuthenticateLogin(cred);
                Assert.IsNotNull(penCred);
        }
        [TestCase("aman@gmail.com","aman" )]
        public void BuildToken_Returns_Object(string username,string password)
        {
            Mock<IAuthenticationManager> mock = new Mock<IAuthenticationManager>();
            mock.Setup(p => p.GetUserList()).Returns(user);
            AuthenticationManager pro = new AuthenticationManager();
            Credentials cred = new Credentials { Email = username, Password = password };
            var penCred = pro.AuthenticateLogin(cred);

            Assert.IsNotNull(penCred);
        }



    }
}