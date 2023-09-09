using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Win32;
using System.Reflection.Emit;
using Treinamento.REST.Domain.Interfaces.Repositories;
using Treinamento.REST.Domain.Enums;

namespace Treinamento.REST.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<User> GetUsers()
        {
            var user = new User();

            var sql = $@"SELECT * FROM dbo.Users;";

            var users = _dbConnection.Query<User>(sql);

            return users;
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
