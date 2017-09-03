using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentity.Identity
{
    public class ApplicationRole
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameNormalized => RoleName?.ToUpper();
    }
}
