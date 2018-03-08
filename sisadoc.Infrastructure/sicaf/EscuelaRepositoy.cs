using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;

namespace sisadoc.Infrastructure.sicaf
{
  public  class EscuelaRepositoy:NHibernateRepository<Escuela>, IEscuelaRepository
    {

      public EscuelaRepositoy() { }
      public IList<Escuela> GetEscuela(int IdUni , int IdFac, int IdEsc)
    
        {
            return Session.QueryOver<Escuela>()
                            .Where(a => a.IdEsc == IdEsc)
                           .And(a => a.FacultadEsc.IdUniversidad== IdUni)
                         .And(a => a.FacultadEsc.Id== IdFac)
                         .List();

        }
    }
}
