using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;
namespace sisadoc.Infrastructure.sicaf
{
   public class UniversidadRepository:NHibernateRepository<Universidad>, IUniverisidadRepository
    {
       public UniversidadRepository() { }
       public IList<Universidad> GetUniversidad(int Id)
        {
            return Session.QueryOver<Universidad>()
                            .Where(a => a.Id == Id)
                         .List();

        }
    }
}
