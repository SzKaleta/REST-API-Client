using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Controllers;
using RestApi.Models.ServerModels;
using RestApi.Models;
using Moq;
using Moq.Protected;
using Autofac.Extras.Moq;
using System.Linq.Expressions;

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

        [TestMethod]
        public void Put_ValidCall()
        {
            //Prepare
            Users mockedUser = new Users()
            {
                user_id = 1,
                name = "John",
                age = 12
            };

            var data = new UserModel()
            {
                user_id = 1,
                age = 30
            };
            int id = 1;
            var baseMock = new Mock<UsersEntitiesData>();
            var userMock = new Mock<ServerController>(baseMock.Object);
            userMock.Protected().Setup<Users>("ReturnById", id).Returns(mockedUser);
            var usersService = userMock.Object;
            // Act
            usersService.Put(id, data);
            // Assert
            baseMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Put_InvalidCall()
        {
            //Prepare
            var data = new UserModel()
            {
                age = 30
            };
            int id = 1;
            var baseMock = new Mock<UsersEntitiesData>();
            var userMock = new Mock<ServerController>(baseMock.Object);
            userMock.Protected().Setup<Users>("ReturnById", id).Returns((Users)null);
            var usersService = userMock.Object;
            // Act
            usersService.Put(id, data);
            // Assert
            baseMock.Verify(x => x.SaveChanges(), Times.Never);
        }
        [TestMethod]
        public void Delete_ValidCall()
        {
            //Prepare
            Users mockedUser = new Users()
            {
                user_id = 1,
                name = "John",
                age = 12
            };

            int id = 1;
            var baseMock = new Mock<UsersEntitiesData>();
            var userMock = new Mock<ServerController>(baseMock.Object);
            userMock.Protected().Setup<Users>("ReturnById", id).Returns(mockedUser);
            baseMock.Setup(x => x.Users.Remove(It.IsAny<Users>())).Returns((Users u) => u);
            var usersService = userMock.Object;
            // Act
            usersService.Delete(id);
            // Assert
            baseMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Delete_InvalidCall()
        {
            //Prepare
            int id = 1;
            var baseMock = new Mock<UsersEntitiesData>();
            var userMock = new Mock<ServerController>(baseMock.Object);
            userMock.Protected().Setup<Users>("ReturnById", id).Returns((Users)null);
            baseMock.Setup(x => x.Users.Remove(It.IsAny<Users>())).Returns((Users u) => u);
            var usersService = userMock.Object;
            // Act
            usersService.Delete(id);
            // Assert
            baseMock.Verify(x => x.SaveChanges(), Times.Never);
        }
    }
}
