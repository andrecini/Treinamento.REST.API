using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Treinamento.REST.Domain.Enums
{
    public enum Roles
    {
        [EnumMember(Value = "None")]
        None,
        [EnumMember(Value = "Administrator")]
        Administrator,
        [EnumMember(Value = "Developer")]
        Developer,
        [EnumMember(Value = "Common")]
        Common
    }
}
