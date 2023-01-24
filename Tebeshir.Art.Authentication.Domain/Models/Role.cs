using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebeshir.Art.Authentication.Domain.Models
{
    public class Role: IdentityRole<Guid>
    {
        public Role(string Name)
        {
            this.Name = Name;
        }
    }
}
