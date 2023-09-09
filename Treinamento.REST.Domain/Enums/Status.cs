using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Treinamento.REST.Domain.Enums
{
    public enum Status
    {
        [EnumMember(Value = "Inactive")]
        Inactive = 0,
        [EnumMember(Value = "Active")]
        Active = 1
    }
}
