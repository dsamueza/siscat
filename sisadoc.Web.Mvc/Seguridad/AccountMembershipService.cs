using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Configuration;
using sisadoc.Tasks.Seguridad;
namespace sisadoc.Web.Mvc.Seguridad
{
 public   class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }


        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Ingrese el usuario", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Ingrese la contraseña", "password");
            return _provider.ValidateUser(userName, password);
        }
    }


}
