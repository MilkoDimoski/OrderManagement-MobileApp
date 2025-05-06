using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PracticeApp.Domain.Models;
using PracticeApp.Repository.Interfaces;

namespace PracticeApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString=configuration.GetConnectionString("DefaultConnection")??String.Empty;
        }

        public async Task<int> AddUser(UserDto user)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Users(Username,Password)
                             VALUES(@Username,@Password)";
                return await connection.ExecuteAsync(sql,user);
            }
        }

        public async Task<int> DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM Users
                            WHERE UserId=@UserId";
                return await connection.ExecuteAsync(sql, new { UserId = id });
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users";
                var users=await connection.QueryAsync<UserDto>(sql);
                return users;
            }
        }

        public async Task<UserDto> GetUsersById(int id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Users
                            WHERE UserId=@UserId";
                var user =await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { UserId = id });
                return user;
            }
        }

        public async Task<int> UpdateUser(UserDto user)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Users
                            SET Username=@Username,
                                Password=@Password
                            WHERE UserId=@UserId";
                var result = await connection.ExecuteAsync(sql, user);
                return result;
            }
        }
    }
}
