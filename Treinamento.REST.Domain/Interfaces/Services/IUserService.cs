using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Entities;
using Treinamento.REST.Domain.Enums;

namespace Treinamento.REST.Domain.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers(int skip, int pageSize);

        User GetUserById(int id);

        int GetTotalAmountOfUsers();

        User AddUser(UserInput userInput);

        User UpdateUser(int id, UserInput userInput);

        bool DeleteUserById(int userId);

        User UpdateUserRole(int userId, Roles role);

        User UpdateUserStatus(int userId, Status status);

        Authentication VerifyUser(string username, string password);
    }
}
