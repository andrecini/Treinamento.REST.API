using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treinamento.REST.Domain.Settings
{
    public static class AppSettings
    {
        public static string Secret { get => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ"; }
        public static string ValidAudience { get => "http://localhost:4200"; }
        public static string ValidIssuer { get => "http://localhost:5000"; }
    }
}
