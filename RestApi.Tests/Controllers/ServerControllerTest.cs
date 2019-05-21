using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Controllers;
using RestApi.Models.ServerModels;
using RestApi.Models;
using Moq;
using Autofac.Extras.Moq;

namespace RestApi.Tests.Controllers
{
    [TestClass]
    public class ServerControllerTest
    {
        [TestMethod]
        public void Post_ValidCall()
        {
            //Prepare
            var userMock = new Mock<UsersEntitiesData>();
            userMock.Setup(x => x.Users.Add(It.IsAny<Users>())).Returns((Users u) => u);

            var usersService = new ServerController(userMock.Object);

            var data = new UserModel()
            {
                name = "Adam",
                age = 81
            };
            // Act
            usersService.Post(data);
            // Assert

            userMock.Verify(x => x.SaveChanges(), Times.Once);

        }

        [TestMethod]
        public void Post_InvalidCall()
        {
            //Prepare
            var userMock = new Mock<UsersEntitiesData>();
            userMock.Setup(x => x.Users.Add(It.IsAny<Users>())).Returns((Users u) => u);

            var usersService = new ServerController(userMock.Object);

            var data = new UserModel()
            {
                age = 300
            };
            // Act
            usersService.Post(data);
            // Assert

            userMock.Verify(x => x.SaveChanges(), Times.Never);

        }
    }
}
