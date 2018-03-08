using System.Collections.Generic;
using sisadoc.Domain.sicaf;
using SharpArch.Domain.PersistenceSupport;
using System;
using sisadoc.Domain.ProcedureClass;

namespace sisadoc.Domain.Reposositories
{
    public interface IActividadDocenteRepository:IRepository<ActividadDocente>
    {
       IList<ActividadDocente> GetActividadDocente(int codPrs);
     /// <summary>
     /// Evento para obtener activades por periodo.
     /// </summary>
     /// <param name="CodPeriodo"></param>
     /// <returns></returns>
        IList<ActividadDocente> GetActividadDocentePeriodo(int CodPeriodo);
        IList<ActividadDocente> GetActividadDocenteOne(int Id);
      bool SaveActividadDocente(ActividadDocente ActividadDoc);
      int ActividadesRealizada(int codPrs, int codPer, DateTime Fin, DateTime ini);
      IList<ActividadDocente> HorasRealizadas(int codPrs, int codPer);
      bool UpdateActividadDocente(int id, ActividadDocente ActividadDoc);
      bool DeleteActividadDocente(int id, string usr_web, string host_web);
      bool EnvioActividadDocente(int CodigoPersona, int CodigoPeriodo, int Mes, int Ano, string usr_web, string host_web, int estado);
      IList<HorasTotalesDocenteSp> obtenerHorasTotales(int CodPersona, int mes, int codPeriodo);

      bool AtendidoActividadDocente(int id, string usr_web, string host_web, int estado);
    }
}
