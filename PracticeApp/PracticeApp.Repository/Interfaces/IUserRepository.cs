using PracticeApp.Domain.Models;

namespace PracticeApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUsersById(int id);
        Task<int> AddUser(UserDto user);    
        Task<int> UpdateUser(UserDto user);
        Task<int> DeleteUser(int id);
        Task<UserDto> GetUserByUsername(string username);
    }
}
