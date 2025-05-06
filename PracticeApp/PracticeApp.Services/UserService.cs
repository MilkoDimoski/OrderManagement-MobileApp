using AutoMapper;
using PracticeApp.Domain.Models;
using PracticeApp.Repository.Interfaces;
using PracticeApp.Services.Interfaces;
using PracticeApp.Services.Models;

namespace PracticeApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<int> AddUser(User user)
        {
            var userDto=_mapper.Map<UserDto>(user);
            var existingUser= await _userRepository.GetUsersById(user.UserId);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }
                return await _userRepository.AddUser(userDto);
        }

        public async Task<int> DeleteUser(int id)
        {
            var existingUser = await _userRepository.GetUsersById(id);
            if (existingUser == null)
            {
                throw new Exception("User does not exist");
            }
            return await  _userRepository.DeleteUser(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var userDtos=await _userRepository.GetAllUsers();
            var users = _mapper.Map<IEnumerable<User>>(userDtos);
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var userDtos=await _userRepository.GetUsersById(id);
            var users=_mapper.Map<User>(userDtos);
            return users;
        }

        public async Task<int> UpdateUser(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);

            var existingUser= await _userRepository.GetUsersById(user.UserId);
            if (existingUser == null)
            {
                throw new Exception("User does not exist");
            }
            return await _userRepository.UpdateUser(userDto);
        }
    }
}
