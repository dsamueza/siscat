using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;
namespace sisadoc.Infrastructure.sicaf
{
  public class FacultadRepository:NHibernateRepository<Facultad>,IFacultadRepository
    {

      public FacultadRepository(){ }
      public IList<Facultad> GetFacultad(int IdUni , int IdFac)
    
        {
            return Session.QueryOver<Facultad>()
                            .Where(a => a.Id == IdFac )
                           .And(a => a.UniversidadFac.Id == IdUni)
                         .List();

        }

    }
}
