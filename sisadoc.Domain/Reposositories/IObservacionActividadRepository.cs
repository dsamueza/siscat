using System.Collections.Generic;
using sisadoc.Domain.sicaf;
using SharpArch.Domain.PersistenceSupport;
using System;
using sisadoc.Domain.ProcedureClass;


namespace sisadoc.Domain.Reposositories
{
 public   interface IObservacionActividadRepository : IRepository<ObservacionActividad>
    {
       
         bool SaveObservacionDocente(ObservacionActividad observacionDocente);
         bool UpdateFechaRevision(DateTime FechaRevision, string usr_web, string host_web, string id, int persona);
         IList<ObservacionActividad> GetObservacioDocente(int CodigoPersona);
    }
}
