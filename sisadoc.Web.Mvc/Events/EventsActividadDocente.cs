using sisadoc.Domain.sicaf;
using sisadoc.Infrastructure.sicaf;
using sisadoc.Tasks.sicaf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sisadoc.Web.Mvc.Utility;
namespace sisadoc.Web.Mvc.Events
{
    public class EventsActividadDocente
    {
        private ActividadDocenteQry actividadDocenteqry = new ActividadDocenteQry(new ActividadDocenteRepository());
        ActividadDocente actividadesIngreso = new ActividadDocente();
        MetodosCM obtenerCM = new MetodosCM();
        public bool InsertarActividadDocente(DateTime DtInicio, DateTime DtFin, string Descripcion, string tipo, string codprs, string codper, string urlArchivos, string usr_cmb_web, string host_cmb_web, string idCliente)
        {
            bool IsInsert = false;

            //if (actividadDocenteqry.ActividadesRealizada(System.Convert.ToInt32(codprs), System.Convert.ToInt32(codper), DtFin, DtInicio) < 1)
            //{
           
            actividadesIngreso.FechaInicio = DtInicio;
            actividadesIngreso.FechaFin = DtFin;
            actividadesIngreso.DescripcionActividad = Descripcion;
            actividadesIngreso.TipoActividad = System.Convert.ToInt32(tipo);
            actividadesIngreso.CodigoPersona = System.Convert.ToInt32(codprs);
            actividadesIngreso.CodigoPeriodo = System.Convert.ToInt32(codper);
            actividadesIngreso.CodigoCliente = System.Convert.ToInt32(idCliente);
            actividadesIngreso.RespaldoDigital = urlArchivos;
            actividadesIngreso.usr_cmb_web = usr_cmb_web;
            actividadesIngreso.usr_hos_web = host_cmb_web;
            actividadDocenteqry.InsertActividadDocente(actividadesIngreso);


            IsInsert = true;
           //}



           return IsInsert; 
        }
        public bool UpdActividadDocente(DateTime DtInicio, DateTime DtFin, string Descripcion, string tipo, string codprs, string codper, string urlArchivos, string usr_cmb_web, string host_cmb_web, string id, string idCliente)
        {
            bool IsInsert = false;

            //if (actividadDocenteqry.ActividadesRealizada(System.Convert.ToInt32(codprs), System.Convert.ToInt32(codper), DtFin, DtInicio) < 1)
            //{

                actividadesIngreso.FechaInicio = DtInicio;
                actividadesIngreso.FechaFin = DtFin;
                actividadesIngreso.DescripcionActividad = Descripcion;
                actividadesIngreso.TipoActividad = System.Convert.ToInt32(tipo);
                actividadesIngreso.CodigoPersona = System.Convert.ToInt32(codprs);
                actividadesIngreso.CodigoPeriodo = System.Convert.ToInt32(codper);
                actividadesIngreso.CodigoCliente = System.Convert.ToInt32(idCliente);
                actividadesIngreso.RespaldoDigital = urlArchivos;
                actividadesIngreso.usr_cmb_web = usr_cmb_web;
                actividadesIngreso.usr_hos_web = host_cmb_web;
                actividadesIngreso.Id = System.Convert.ToInt32(id);
           


                IsInsert = actividadDocenteqry.ActividadesRealizadaUpd(System.Convert.ToInt32(id), actividadesIngreso);
            //}



            return IsInsert;
        }
        public bool EnviarActividadDocente(string MesAno, int periodo, int codprs, string usr_cmb_web, string host_cmb_web, int estado)
        {
            bool IsInsert = false;
            


            IsInsert = actividadDocenteqry.CambiarEstadoActividadDocente(codprs, periodo, obtenerCM.GetSepararCarracteres(MesAno, "_", 0), obtenerCM.GetSepararCarracteres(MesAno, "_", 1),usr_cmb_web,host_cmb_web,estado);
           



            return IsInsert;
        }
        public bool ElmActividadDocente(string usr_cmb_web, string host_cmb_web, string id)
        {
            bool IsInsert = false;
           


            IsInsert = actividadDocenteqry.ActividadesRealizadaElm(System.Convert.ToInt32(id), usr_cmb_web, host_cmb_web);
            



            return IsInsert;
        }
    }
}