using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Entities;
using Treinamento.REST.Domain.Enums;

namespace Treinamento.REST.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUserById(int id);

        bool AddUser(User user);

        bool UpdateUser(User user);

        bool DeleteUserById(int userId);

        bool UpdateUserRole(int userId, Roles role);

        Authentication VerifyUser(string username);
    }
}
