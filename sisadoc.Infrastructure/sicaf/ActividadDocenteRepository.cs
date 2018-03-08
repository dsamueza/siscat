using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;
using System;
using System.Text;

namespace sisadoc.Infrastructure.sicaf
{
  public  class ActividadDocenteRepository:NHibernateRepository<ActividadDocente>, IActividadDocenteRepository
    {
           public ActividadDocenteRepository() { }
           private string NombreBase = "sisadoc.dbo.";
      public IList<ActividadDocente> GetActividadDocente(int IdPrs)
    
        {
            return Session.QueryOver<ActividadDocente>()
                            .Where(a => a.PersonaActividad.Id==IdPrs)
                            .List();

        }

      public IList<ActividadDocente> GetActividadDocenteOne(int ID)
      {
          return Session.QueryOver<ActividadDocente>()
                          .Where(a => a.Id==ID)
                          .List();

      }
      /// <summary>
      /// Obtiene el todos las actividades por periodo 
      /// </summary>
      /// <param name="codPeriodo"></param>
      /// <returns></returns>
      public IList<ActividadDocente> GetActividadDocentePeriodo(int codPeriodo)
      {
          return Session.QueryOver<ActividadDocente>()
                          .Where(a => a.CodigoPeriodo == codPeriodo)
                          .List();

      }
      public IList<HorasTotalesDocenteSp> obtenerHorasTotales(int CodPersona, int mes, int codPeriodo)
      {

          IList<HorasTotalesDocenteSp> resultsp = new List<HorasTotalesDocenteSp>();
          var res = Session.GetNamedQuery("sp_horasxActividadDocente")
                     .SetInt32("prs_cod", CodPersona)
                     .SetInt32("mes", mes)
                     .SetInt32("per_cod", codPeriodo)
                     .SetResultTransformer(new AliasToBeanResultTransformer(typeof(HorasTotalesDocenteSp)));


          //if (res.Count > 0) {

          //    resultsp.Add(new RolPersonaSp { NivelPersona = Convert.ToInt32(res [01].ToString()), UnidadOrganizacion = Convert.ToInt32(res) });

          //}


          return res.List<HorasTotalesDocenteSp>();

      }
       public bool EnvioActividadDocente(int CodigoPersona, int CodigoPeriodo, int Mes , int Ano , string usr_web, string host_web, int estado) 
      {
         
              StringBuilder sb = new StringBuilder();
              try {
                  sb = new StringBuilder("UPDATE  " + NombreBase+ "sa_act_docente" +
                                       " SET sa_act_doc_sta = :ParSta ," +
                                
                                       " usr_cmb_web = :ParUsr ," +
                                       " usr_hos_web = :ParHost " +
                                       " WHERE   si_prs_cod = :CodActividad and " +
                                                 " sa_per_cod = :PerCod and " +
                                                 "MONTH(sa_act_fch_fin) = :month  and" +
                                                 " YEAR(sa_act_fch_fin) = :year" +
                                                 " AND sa_act_doc_sta NOT IN (3) ");
              var query = Session.CreateSQLQuery(sb.ToString())
                                 .SetInt32("ParSta", estado)
                                 .SetInt32("year", Ano)
                                 .SetString("ParUsr", usr_web)
                                 .SetString("ParHost", host_web)
                                 .SetInt32("CodActividad", CodigoPersona)
                                 .SetInt32("PerCod", CodigoPeriodo)
                                 .SetInt32("month", Mes);
              query.ExecuteUpdate();

              return true;
              } catch( Exception e)
              {
                   return false;
              }
          
      }


       public bool AtendidoActividadDocente(int id ,string usr_web, string host_web, int estado)
       {

           StringBuilder sb = new StringBuilder();
           try
           {
               sb = new StringBuilder("UPDATE  " + NombreBase + "sa_act_docente" +
                                    " SET sa_act_doc_sta = :ParSta ," +
                                    " usr_cmb_web = :ParUsr ," +
                                    " usr_hos_web = :ParHost " +
                                    " WHERE   sa_act_doc_cod = :CodActividad  " );
               var query = Session.CreateSQLQuery(sb.ToString())
                                  .SetInt32("ParSta", estado)
                                  .SetString("ParUsr", usr_web)
                                  .SetString("ParHost", host_web)
                                  .SetInt32("CodActividad", id);
                       
               query.ExecuteUpdate();

               return true;
           }
           catch (Exception e)
           {
               return false;
           }

       }
      public bool SaveActividadDocente(ActividadDocente insertactividadDocente)
      {
          Session.Save(insertactividadDocente);    

          return true;

      }
      public bool UpdateActividadDocente(int id, ActividadDocente insertactividadDocente)
      {
        
          try
          {
              StringBuilder sb = new StringBuilder();

              sb = new StringBuilder("UPDATE " + NombreBase + "sa_act_docente" +
                                       " SET sa_act_doc_desc = :ParDesc ," +
                                       " sa_act_tpo_cod = :ParTipo ,"+
                                       " sa_act_fch_inicio =:ParInicio  ," +
                                       " sa_act_fch_fin = :ParFIN  ," +
                                       " usr_cmb_web = :ParUsr ," + 
                                       " usr_hos_web = :ParHost ," +
                                       "sa_act_doc_respado = :RespaldoUrl , " +
                                       "sa_cli_cod = :sa_cli " +
                                       " WHERE   sa_act_doc_cod = :CodActividad");
            var query = Session.CreateSQLQuery(sb.ToString())
                               .SetString("ParDesc", insertactividadDocente.DescripcionActividad)
                               .SetInt32("ParTipo", insertactividadDocente.TipoActividad)
                               .SetDateTime("ParInicio", insertactividadDocente.FechaInicio)
                               .SetDateTime("ParFIN", insertactividadDocente.FechaFin)
                               .SetString("ParUsr", insertactividadDocente.usr_cmb_web)
                               .SetString("ParHost", insertactividadDocente.usr_hos_web)
                               .SetString("RespaldoUrl", insertactividadDocente.RespaldoDigital)
                               .SetInt32("sa_cli", insertactividadDocente.CodigoCliente)
                               .SetInt32("CodActividad", id);
            query.ExecuteUpdate();
         
              return true;
          }
          catch (Exception)
          {
              return false;

          }
          

      }
      /// <summary>
      /// Elimina los registro de actividades
      /// </summary>
      /// <param name="codPr">Codigo de la actividad</param>
      public bool DeleteActividadDocente(int id, string usr_web, string host_web)
      {

          try
          {
              /// <summary>
              /// Actualiza para campos de auditoria
              /// </summary>
              /// <param name="codPr">Codigo de la actividad</param>
              StringBuilder sb1 = new StringBuilder();

              sb1 = new StringBuilder("UPDATE  " + NombreBase + "sa_act_docente" +
                                       " SET usr_cmb_web = :ParUsr ," +
                                       " usr_hos_web = :ParHost " +
                                       " WHERE   sa_act_doc_cod = :CodActividad");
              var queryup = Session.CreateSQLQuery(sb1.ToString())
                                  .SetString("ParUsr", usr_web)
                                 .SetString("ParHost", host_web)
                                 .SetInt32("CodActividad", id);
              queryup.ExecuteUpdate();
   
              StringBuilder sb = new StringBuilder();

              sb = new StringBuilder("DELETE FROM " + NombreBase +"sa_act_docente WHERE   sa_act_doc_cod = :CodActividad");
              var query = Session.CreateSQLQuery(sb.ToString())
                                 .SetInt32("CodActividad", id);
              query.ExecuteUpdate();

              return true;

          }catch(Exception)
          {
           return false;
          
          }

      }

      public int ActividadesRealizada(int codPrs, int codPer, DateTime Fin, DateTime ini) {

          int count = Session.QueryOver<ActividadDocente>()
                         .Where(x => x.CodigoPersona == codPrs)
                         .And(x => x.CodigoPeriodo == codPer)
                         .And(x => x.FechaInicio < ini && x.FechaFin > ini
                              || x.FechaInicio < Fin && x.FechaFin > Fin
                              || x.FechaInicio > ini && x.FechaFin < Fin)
                         .RowCount();

          return count;

      
      }
      public IList<ActividadDocente> HorasRealizadas(int codPrs, int codPer)
      {
        

          return  Session.QueryOver<ActividadDocente>()
              .Where(x => x.CodigoPersona == codPrs)
              .And(x => x.CodigoPeriodo == codPer)
              .List();

      }
    }
    }

