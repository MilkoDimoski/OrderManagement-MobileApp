using AutoMapper;
using NSubstitute;
using PracticeApp.Domain.Models;
using PracticeApp.Repository.Interfaces;
using PracticeApp.Services;
using PracticeApp.Services.Models;
using Xunit;

namespace PracticeApp.Tests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository =Substitute.For<IUserRepository>();
        private readonly IMapper _mapperMock=Substitute.For<IMapper>();
        
        
        [Fact]
        public async Task GetUserById_ShouldReturnUser()
        {
            var userDto = new UserDto
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            _userRepository.GetUsersById(userDto.UserId).Returns(userDto);
            _mapperMock.Map<User>(userDto).Returns(user);

            var sut=new UserService(_userRepository, _mapperMock);
            var result=await sut.GetUserById(userDto.UserId);
            Assert.NotNull(result);
            Assert.Equal(userDto.Username, result.Username);
            Assert.Equal(userDto.Password, result.Password);
            Assert.Equal(userDto.UserId, result.UserId);
        }
        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExists()
        {
            _userRepository.GetUsersById(Arg.Any<int>()).Returns((UserDto)null);

            var sut = new UserService(_userRepository, _mapperMock);
            var result = await sut.GetUserById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnListOfUsers()
        {
            var userDtos = new List<UserDto>
            {
                new UserDto
                {
                    UserId = 1,
                    Username = "testuser1",
                    Password = "testpassword1",
                },
                new UserDto
                {
                    UserId = 2,
                    Username = "testuser2",
                    Password = "testpassword2",
                }
            };
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    Username = "testuser1",
                    Password = "testpassword1",
                },
                new User
                {
                    UserId = 2,
                    Username = "testuser2",
                    Password = "testpassword2",
                }
            };
            _userRepository.GetAllUsers().Returns(userDtos);
            _mapperMock.Map<IEnumerable<User>>(userDtos).Returns(users);
            var sut = new UserService(_userRepository, _mapperMock);
            var result = await sut.GetAllUsers();
            Assert.NotNull(result);
            Assert.Equal(userDtos.Count, result.Count());
        }
        [Fact]
        public async Task GetAllUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            _userRepository.GetAllUsers().Returns(new List<UserDto>());
            _mapperMock.Map<IEnumerable<User>>(Arg.Any<IEnumerable<UserDto>>()).Returns(new List<User>());

            var sut = new UserService(_userRepository, _mapperMock);
            var result = await sut.GetAllUsers();

            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public async Task AddUser_ShouldReturn1_WhenUserIsAdded()
        {
            var userDto = new UserDto
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            _userRepository.GetUsersById(userDto.UserId).Returns((UserDto)null);
            _mapperMock.Map<UserDto>(user).Returns(userDto);
            _userRepository.AddUser(userDto).Returns(1);
            var sut = new UserService(_userRepository, _mapperMock);
            var result = await sut.AddUser(user);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task AddUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            var userDto = new UserDto
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            _userRepository.GetUsersById(userDto.UserId).Returns(userDto);
            _mapperMock.Map<UserDto>(user).Returns(userDto);
            var sut = new UserService(_userRepository, _mapperMock);
            await Assert.ThrowsAsync<Exception>(() => sut.AddUser(user));
        }
        [Fact]
        public async Task UpdateUser_ShouldReturn1_WhenUserIsUpdated()
        {
            var userDto = new UserDto
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            _userRepository.GetUsersById(userDto.UserId).Returns(userDto);
            _mapperMock.Map<UserDto>(user).Returns(userDto);
            _userRepository.UpdateUser(userDto).Returns(1);
            var sut = new UserService(_userRepository, _mapperMock);
            var result = await sut.UpdateUser(user);
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task UpdateUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            var userDto = new UserDto
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            _userRepository.GetUsersById(userDto.UserId).Returns((UserDto)null);
            _mapperMock.Map<UserDto>(user).Returns(userDto);
            var sut = new UserService(_userRepository, _mapperMock);
            await Assert.ThrowsAsync<Exception>(() => sut.UpdateUser(user));
        }

        [Fact]
        public async Task DeleteUser_ShouldReturn1_WhenUserIsDeleted()
        {
            var userDto = new UserDto
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            _userRepository.GetUsersById(userDto.UserId).Returns(userDto);
            _mapperMock.Map<UserDto>(user).Returns(userDto);
            _userRepository.DeleteUser(userDto.UserId).Returns(1);
            var sut = new UserService(_userRepository, _mapperMock);
            var result = await sut.DeleteUser(user.UserId);
            Assert.Equal(1, result);
        }
        [Fact]
        public async Task DeleteUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            var userDto = new UserDto
            {
                UserId = 1,
                Username = "testuser",
                Password = "testpassword",
            };
            var user = new User
            {
                UserId = 1, 
                Username = "testuser",
                Password = "testpassword",
            };
            _userRepository.GetUsersById(userDto.UserId).Returns((UserDto)null);
            _mapperMock.Map<UserDto>(user).Returns(userDto);
            var sut = new UserService(_userRepository, _mapperMock);
            await Assert.ThrowsAsync<Exception>(() => sut.DeleteUser(user.UserId));
        }
    }
}
    
