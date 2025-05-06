using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PracticeApp.API.Controllers;
using PracticeApp.Services.Interfaces;
using PracticeApp.Services.Models;
using Xunit;

namespace PracticeApp.Tests.ApiTests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAllUsers_ReturnsOkResult()
        {
            var mockUserService = Substitute.For<IUserService>();
            var users = new List<User>()
            {
                new User { UserId = 1, Username = "user1", Password = "pass1" },
                new User { UserId = 2, Username = "user1", Password = "pass1" }
            };
            mockUserService.GetAllUsers().Returns(users);
            var controller = new UserController(mockUserService);
            var result = await controller.GetAllUsers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUsers = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, returnUsers.Count);
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult()
        {
            var mockUserService = Substitute.For<IUserService>();
            var user = new User { UserId = 1, Username = "user1", Password = "pass1" };

            mockUserService.GetUserById(1).Returns(user);
            var controller = new UserController(mockUserService);

            var result = await controller.GetUserById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("user1", returnUser.Username);
        }
        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            var mockUserService = Substitute.For<IUserService>();
            mockUserService.GetUserById(1).Returns((User)null);
            var controller = new UserController(mockUserService);
            var result = await controller.GetUserById(1);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task AddUser_ShouldReturnValidUser()
        {
            var mockUserService = Substitute.For<IUserService>();
            var user = new User { UserId = 1, Username = "user1", Password = "pass1" };

            mockUserService.AddUser(user).Returns(1);
            var controller = new UserController(mockUserService);
            var result = await controller.AddUser(user);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnUser = Assert.IsType<User>(createdResult.Value);
            Assert.Equal("user1", returnUser.Username);
        }
        [Fact]
        public async Task AddUser_ShouldReturnBadRequest_WhenUserIsInvalid()
        {
            var mockUserService = Substitute.For<IUserService>();
            var user = new User { UserId = 0, Username = "user1", Password = "pass1" };
            mockUserService.AddUser(user).Returns(0);
            var controller = new UserController(mockUserService);
            var result = await controller.AddUser(user);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task UpdateUser_ShouldReturnOkResult()
        {
            var mockUserService = Substitute.For<IUserService>();
            var user = new User { UserId = 1, Username = "user1", Password = "pass1" };

            mockUserService.GetUserById(1).Returns(user);
            var controller = new UserController(mockUserService);
            var result = await controller.UpdateUser(user);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("user1", returnUser.Username);
        }
        [Fact]
        public async Task UpdateUser_ShouldReturnBadRequest_WhenUserIsNull()
        {
            var mockUserService = Substitute.For<IUserService>();
            var user = new User { UserId = 0, Username = "user1", Password = "pass1" };
            mockUserService.GetUserById(0).Returns((User)null);
            var controller = new UserController(mockUserService);
            var result = await controller.UpdateUser(user);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task DeleteUser_ShouldReturnOkResult()
        {
            var mockUserService = Substitute.For<IUserService>();
            var user = new User { UserId = 1, Username = "user1", Password = "pass1" };

            mockUserService.GetUserById(1).Returns(user);
            var controller = new UserController(mockUserService);
            var result = await controller.DeleteUser(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("user1", returnUser.Username);
        }
    }
}
