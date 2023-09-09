using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Enums;

namespace Treinamento.REST.Domain.Entities
{
    [DisplayName("Users")]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastUpdate { get; set; }
        public Roles Role { get; set; }
        public Status Active { get; set; }

        public static User Map(UserInput user) 
        {
            return new User()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                LastUpdate = DateTime.Now,
            };
        }

        public static User Map(int id, UserInput user)
        {
            return new User()
            {
                Id = id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                LastUpdate = DateTime.Now
            };            
        }
    }
}
