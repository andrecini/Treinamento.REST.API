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
            var sql = $@"SELECT * FROM dbo.Users WHERE UserID = @Id;";

            var user = _dbConnection.QueryFirstOrDefault<User>(sql, new { Id = id });

            return user;
        }

        public bool AddUser(User user)
        {
            var sql = $@"INSERT INTO dbo.Users
                         (
                            UserName,
                            Email,
                            PasswordHash,
                            RegistrationDate
                         )
                         VALUES
                         (
                            @UserName,
                            @Email,
                            @PasswordHash,
                            @RegistrationDate
                         );";

            var qtdLinhasAfetadas = _dbConnection.Execute(sql, user);

            return qtdLinhasAfetadas > 0;
        }

        public bool UpdateUser(User user)
        {
            var sql = $@"UPDATE dbo.Users
                         SET
                            UserName = @UserName,
                            Email = @Email,
                            PasswordHash = @PasswordHash,
                            RegistrationDate = @RegistrationDate
                         WHERE
                            UserID = @UserID;";

            var qtdLinhasAfetadas = _dbConnection.Execute(sql, user);

            return qtdLinhasAfetadas > 0;
        }

        public bool DeleteUserById(int userId)
        {
            var sql = "DELETE FROM dbo.Users WHERE UserID = @UserID";

            var qtdLinhasAfetadas = _dbConnection.Execute(sql, new { UserID = userId });

            return qtdLinhasAfetadas > 0;
        }

        public bool UpdateUserRole(int userId, Roles role)
        {
            var sql = $@"UPDATE dbo.Users
                         SET
                            Role = @Role
                         WHERE
                            UserID = @UserID;";

            var qtdLinhasAfetadas = _dbConnection.Execute(sql, new { Role = role, UserID = userId});

            return qtdLinhasAfetadas > 0;
        }

        public Authentication VerifyUser(string username)
        {
            var sql = $@"SELECT * FROM dbo.Users WHERE UserName = @UserName;";

            var auth = _dbConnection.QueryFirstOrDefault<Authentication>(sql, new { UserName = username });

            return auth;
        }
    }
}
