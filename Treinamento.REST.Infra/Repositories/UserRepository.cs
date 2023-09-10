using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;
using Treinamento.REST.Domain.Entities;
using Treinamento.REST.Domain.Enums;
using Treinamento.REST.Domain.Interfaces.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Treinamento.REST.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<User> GetUsers(int skip, int pageSize)
        {
            var sql = $@"SELECT* FROM dbo.Users
                         ORDER BY Id
                         OFFSET @Skip
                         ROWS
                         FETCH NEXT @PageSize
                         ROWS ONLY;";

            var users = _dbConnection.Query<User>(sql, new { Skip = skip, PageSize = pageSize});

            return users;
        }
        public int GetTotalAmountOfUsers()
        {
            var sql = $@"SELECT COUNT(*) FROM dbo.Users";

            var total = _dbConnection.QueryFirst<int>(sql);

            return total;
        }

        public User GetUserById(int id)
        {
            var sql = $@"SELECT * FROM dbo.Users WHERE Id = @Id;";

            var user = _dbConnection.QueryFirstOrDefault<User>(sql, new { Id = id });

            return user;
        }

        public User AddUser(User user)
        {
            var sql = $@"INSERT INTO dbo.Users
                         (
                            Username,
                            Email,
                            Password,
                            LastUpdate
                         )
                         OUTPUT INSERTED.Id
                         VALUES
                         (
                            @Username,
                            @Email,
                            @Password,
                            @LastUpdate
                         );";

            var newId = _dbConnection.QuerySingle<int>(sql, user);
            
            var newUser = GetUserById(newId);

            return newUser;
        }

        public User UpdateUser(User user)
        {
            var sql = $@"UPDATE dbo.Users
                 SET
                    Username = @Username,
                    Email = @Email,
                    Password = @Password,
                    LastUpdate = @LastUpdate
                 OUTPUT INSERTED.Id
                 WHERE
                    Id = @Id;";

            var Id = _dbConnection.QuerySingle<int>(sql, user);

            return GetUserById(Id);
        }


        public bool DeleteUserById(int Id)
        {
            var sql = "DELETE FROM dbo.Users WHERE Id = @Id";

            var qtdLinhasAfetadas = _dbConnection.Execute(sql, new { Id = Id });

            return qtdLinhasAfetadas > 0;
        }

        public User UpdateUserRole(int Id, Roles role)
        {
            var sql = $@"UPDATE dbo.Users
                         SET
                            Role = @Role
                         WHERE
                            Id = @Id;";

            var qtdLinhasAfetadas = _dbConnection.Execute(sql, new { Role = role, Id = Id});

            var userUpdated = GetUserById(Id);

            return userUpdated;
        }

        public User UpdateUserStatus(int Id, Status status)
        {
            var sql = $@"UPDATE dbo.Users
                         SET
                            Active = @Status
                         WHERE
                            Id = @Id;";

            var qtdLinhasAfetadas = _dbConnection.Execute(sql, new { Status = status, Id = Id });

            var userUpdated = GetUserById(Id);

            return userUpdated;
        }

        public Authentication VerifyUser(string Username)
        {
            var sql = $@"SELECT * FROM dbo.Users WHERE Username = @Username;";

            var auth = _dbConnection.QueryFirstOrDefault<Authentication>(sql, new { Username = Username });

            return auth;
        }
    }
}
