using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Entities;
using Treinamento.REST.Domain.Enums;
using Treinamento.REST.Domain.Interfaces.Repositories;
using Treinamento.REST.Domain.Interfaces.Services;
using Treinamento.REST.Domain.Settings;

namespace Treinamento.REST.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptorService _encryptorService;

        public UserService(IUserRepository userRepository, IEncryptorService encryptorService)
        {
            _userRepository = userRepository;
            _encryptorService = encryptorService;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetUsers();

            foreach (var user in users)
            {
                user.PasswordHash = _encryptorService.Decrypt(user.PasswordHash);
            }

            return users;
        }

        public User GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user != null)
            {
            user.PasswordHash = _encryptorService.Decrypt(user.PasswordHash);
            }

            return user;
        }

        public bool AddUser(User user)
        {
            user.PasswordHash = _encryptorService.Encrypt(user.PasswordHash);

            return _userRepository.AddUser(user);
        }

        public bool UpdateUser(User user)
        {
            user.PasswordHash = _encryptorService.Encrypt(user.PasswordHash);

            return _userRepository.UpdateUser(user);
        }

        public bool DeleteUserById(int userId)
        {
            return _userRepository.DeleteUserById(userId);
        }

        public bool UpdateUserRole(int userId, Roles role)
        {
            return _userRepository.UpdateUserRole(userId, role);
        }

        public Authentication VerifyUser(string username, string password)
        {
            var auth = _userRepository.VerifyUser(username);
            var user = _userRepository.GetUserById(auth == null ? 0 : auth.UserID);


            if (user != null)
            {
                var passwordDecrypted = _encryptorService.Decrypt(user.PasswordHash);

                if (passwordDecrypted != password)
                    return null;

                auth.Token = generateJwtToken(user);

                return auth;
            }

            return null;
        }

        private TokenModel generateJwtToken(User user)
        {
            var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AppSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: AppSettings.ValidIssuer,
                audience: AppSettings.ValidAudience,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
