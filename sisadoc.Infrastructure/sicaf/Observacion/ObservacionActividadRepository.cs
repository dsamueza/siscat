using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;
using System;
using System.Text;

namespace sisadoc.Infrastructure.sicaf.Observacion
{
   public class ObservacionActividadRepository : NHibernateRepository<ObservacionActividad>, IObservacionActividadRepository
    {
       private string NombreBase = "sisadoc.dbo.";
       public ObservacionActividadRepository() { }
       /// <summary>
       /// Metodo para guardar las observaciones enviadas por el docenete coordinador
       /// </summary>
       /// <param name="observacionDocente"></param>
       /// <returns></returns>
       public bool SaveObservacionDocente(ObservacionActividad observacionDocente)
        {
          
            observacionDocente.FechaLectura = null;
            Session.Save(observacionDocente);
       
            return true;

        }
       /// <summary>
       /// Obtiene todas las observaciones del docentes que no haya sido corregidas.
       /// </summary>
       /// <param name="CodigoPersona"></param>
       /// <returns></returns>
     public  IList<ObservacionActividad> GetObservacioDocente(int CodigoPersona){
         return Session.QueryOver<ObservacionActividad>()
                          .Where(a => a.CodigoPersonaDestinatario == CodigoPersona  )
                          .List();
       
       }
       /// <summary>
       /// Actualiza la fecha de revision
       /// </summary>
       /// <param name="FechaRevision"></param>
       /// <returns></returns>
     public bool UpdateFechaRevision(DateTime FechaRevision, string usr_web, string host_web, string id, int persona)
     {

         StringBuilder sb1 = new StringBuilder();

         sb1 = new StringBuilder("UPDATE  " + NombreBase + "sa_act_observacion" +
                                  " SET usr_cmb_web = :ParUsr ," +
                                  " usr_hos_web = :ParHost , " +
                                  "sa_act_obs_fch_lec= :FchLec " +
                                  " WHERE   sa_act_obs_mes = :CodActividad" +
                                            " and si_prs_cod_rec = :Codpers");
         var queryup = Session.CreateSQLQuery(sb1.ToString())
                            .SetString("ParUsr", usr_web)
                            .SetString("ParHost", host_web)
                            .SetDateTime("FchLec", FechaRevision)
                            .SetString("CodActividad", id)
                            .SetInt32("Codpers", persona);
         queryup.ExecuteUpdate();
   
         return true;
     }
    }
}
