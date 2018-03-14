using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;

namespace sisadoc.Tasks.sicaf
{
   public class ActividadDocenteQry
    {

       private readonly IActividadDocenteRepository IActividadDocente;
      
      public ActividadDocenteQry(IActividadDocenteRepository iActividadDocente)
        {
            this.IActividadDocente = iActividadDocente;
      
        }

      public IList<ActividadDocente> GetOneActividad(int Id)
      {
          return IActividadDocente.GetActividadDocenteOne(Id);
      }
      /// <summary>
      /// Ingresa los datos de inscripción
      /// </summary>
      /// <param name="car_ute">Datos de inscripción</param>
      public IList<ActividadDocente> ObtenerActividadDocente(int IdPrs)
      {
          return IActividadDocente.GetActividadDocente(IdPrs);
      }

        public IList<ActividadDocente> ObtenerActividad(bool all , int Estado)
        {
            return IActividadDocente.GetActividadDocenteAll(all,Estado);
        }
        /// <summary>
        /// Ingresa los datos de inscripción
        /// </summary>
        /// <param name="car_ute">Datos de inscripción</param>
        public bool InsertActividadDocente(ActividadDocente IdPrs)
      {
          return IActividadDocente.SaveActividadDocente(IdPrs);
      }

      /// <summary>
      /// Actualiza el estado de la de envio 1,3= estado finales y 2,0= estdos pediente
      /// </summary>
      /// <param name="CodigoPersona">codigo de persona</param>
      /// <param name="CodigoPeriodo">codigo de periodo</param>
      /// <param name="Mes">mes de envio</param>
      /// <param name="Ano">ano de envio</param>
      /// <param name="estado">Estado de envio</param>
      public bool CambiarEstadoActividadDocente(int CodigoPersona, int CodigoPeriodo, int Mes, int Ano, string usr_web, string host_web, int estado) 
      {
          return IActividadDocente.EnvioActividadDocente(CodigoPersona, CodigoPeriodo,  Mes,  Ano,  usr_web,  host_web,  estado);
      }


      public bool AtEstadoActividadDocente(int CodigoPersona,  string usr_web, string host_web, int estado)
      {
          return IActividadDocente.AtendidoActividadDocente(CodigoPersona ,usr_web, host_web, estado);
      }
      /// <summary>
      /// Ingresa los datos de inscripción
      /// </summary>
      /// <param name="car_ute">Datos de inscripción</param>
      public int ActividadesRealizada(int codPrs, int codPer, DateTime Fin, DateTime ini)
      {
          return IActividadDocente.ActividadesRealizada(codPrs, codPer, Fin, ini);
      }
      /// <summary>
      /// Acttualiza los registro de actividades
      /// </summary>
      /// <param name="car_ute">Datos de inscripción</param>
      public bool ActividadesRealizadaUpd(int codPrs ,ActividadDocente ActividadesDocente)
      {
          return IActividadDocente.UpdateActividadDocente(codPrs, ActividadesDocente);
      }
      /// <summary>
      /// Elimina los registro de actividades
      /// </summary>
      /// <param name="codPr">Codigo de la actividad</param>
      public bool ActividadesRealizadaElm(int codPrs, string usr_web, string host_web)
      {
          return IActividadDocente.DeleteActividadDocente(codPrs, usr_web, host_web);
      }
            /// <summary>
      /// Obtienes los el total de horas realizadas
      /// </summary>
      /// <param name="car_ute">Datos de inscripción</param>
      public IList<HorasTotalesDocenteSp>   obtenerHorasTotales(int CodPersona, int mes, int codPeriodo)
      {
          return IActividadDocente.obtenerHorasTotales(CodPersona,mes,codPeriodo);
      }


        public IList<CountActividadSp> obtenerNumeroActividades(DateTime begin, DateTime end)
        {
            return IActividadDocente.GetCountActiviCountActividad(begin, end);
        }

        public IList<ActividadTipoSp> ObtenerPorcentaAct(DateTime begin, DateTime end)
        {
            return IActividadDocente.GetPorcentActTipo(begin, end);
        }
        public IList<ActividadYear> obtenerNumeroXYear(DateTime begin, DateTime end)
        {
            return IActividadDocente.GetCountActiviYear(begin, end);
        }
        /// <summary>
        /// Obtienes todos los registro del docente en el periodo
        /// </summary>
        /// <param name="codPrs">Codigo Persona</param>
        /// <param name="codPer">Codigo Periodo</param>
        public IList<ActividadDocente> HorasRealizadas(int codPrs, int codPer)
      {
          return IActividadDocente.HorasRealizadas(codPrs, codPer);
      }
       /// <summary>
       /// Obtiene las actividades realizadas dentro del periodo
       /// </summary>
      /// <param name="IdPeriodo"></param>
       /// <returns></returns>
      public IList<ActividadDocente> GetActividadDocentePeriodo(int IdPeriodo)
      {
          return IActividadDocente.GetActividadDocentePeriodo(IdPeriodo);
      }

       
    }
}
