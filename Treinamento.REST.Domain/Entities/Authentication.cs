using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Enums;

namespace Treinamento.REST.Domain.Entities
{
    public class Authentication
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public Roles Role { get; set; }
        public TokenModel Token { get; set; }
    }
}
