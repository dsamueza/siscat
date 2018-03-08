using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;
namespace sisadoc.Infrastructure.sicaf
{
    public class ClienteRepository : NHibernateRepository<Cliente>, IClienteRepository
    {


       public ClienteRepository() { }
        public IList<Cliente> obtenerPersona()
        {
            return Session.QueryOver<Cliente>()
                         
                         .List();

        }
        
    }


}
