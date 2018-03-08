using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;
using System;
namespace sisadoc.Infrastructure.sicaf
{
  public  class PeriodoRepository:NHibernateRepository<Periodo>,IPeriodoRepository
    {

      public IList<Periodo> GetPeriodo(int iduni, int idfac, int idesc)
      {
           DateTime saveNow = DateTime.Now;
          return Session.QueryOver<Periodo>()
                          .Where(a => a.IdEscP == idesc 
                                    && a.IdFacultadP == idfac 
                                    && a.IdUniversidadP == iduni
                                    && a.PeriodoAbierto==1
                                    && a.PeriodoPropedeutico!=1
                                    && a.PeriodoFin > saveNow)
             
                          .List();

      }
    }
}
