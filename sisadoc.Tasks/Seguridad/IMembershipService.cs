using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Tasks.Seguridad
{
    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
    }

}
