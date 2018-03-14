using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sisadoc.Tasks.sicaf;
using sisadoc.Infrastructure.sicaf;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using System.Configuration;
using sisadoc.Web.Mvc.Models;
using System.IO;
using System.Text;
using sisadoc.Web.Mvc.Utility;
using sisadoc.Web.Mvc.Events;
using ICSharpCode.SharpZipLib;
using System.Drawing;
namespace sisadoc.Web.Mvc.Controllers
{
    public class DashboardController : SisadocController
    {
        //
        // GET: /Dashboard/
        //
        // GET: /Docente/
        //  private ActividadDocenteQry actividadDocenteqry = new ActividadDocenteQry(new ActividadDocenteRepository());
        //    private EventsActividadDocente eventoActividades = new EventsActividadDocente();
        private CarreraQry carreraqry = new CarreraQry(new UniversidadRepository(), new FacultadRepository(), new EscuelaRepositoy(), new PeriodoRepository());
        private IList<Universidad> lstuniversidad = new List<Universidad>();
        private IList<Facultad> lstfacultad = new List<Facultad>();
        private IList<Escuela> lstactividadDocente = new List<Escuela>();
        private IList<Periodo> lstPeriodo = new List<Periodo>();
        //  private IList<ActividadDocente> lstatividadRealizada= new List<ActividadDocente>();
        private IList<CarreraDocenteSp> lstcarreraDocenteSP = new List<CarreraDocenteSp>();
        private IList<HorasTotalesDocenteSp> lstHorasTotalSP = new List<HorasTotalesDocenteSp>();
        private IList<ListModel> lstObs = new List<ListModel>();
        private ClienteQry _clienteDAO = new ClienteQry(new ClienteRepository());
        private ActividadDocenteQry _ActividadDO = new ActividadDocenteQry(new ActividadDocenteRepository());
        public ActionResult Index()
        {

            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                   
                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.Menu = Session[Smenu];
                    ViewBag.tipoUsuario = "Coordinador";
                    lstObs = EventObservacion.CabMensajes(System.Convert.ToInt32(Session[CodPrs]));
                    //lstObservaciones = observacionesQry.RecuperarObservacioDocente(System.Convert.ToInt32(Session[CodPrs]));
                    //Session[Smsg] = lstObservaciones.Count;
                    ViewBag.Msg = lstObs.Count;
                    ViewBag.Smsglst = lstObs;
                    IList<CountActividadSp> model = _ActividadDO.obtenerNumeroActividades(DateTime.Parse("2018/01/01 00:00:00"), DateTime.Parse("2019/01/01 00:00:00"));
                    return View(model);
                }
            }


            return RedirectToAction("LogOff", "LogOn");

        }
        public ActionResult GetSpanNumber(string Id, string range)
        {
            if (Session[CodPrs] != null)
            {

                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    String value = range;
                    Char delimiter = '&';
                    String[] rang = value.Split(delimiter);
                    IList<CountActividadSp> model = _ActividadDO.obtenerNumeroActividades(DateTime.Parse(rang[0]), DateTime.Parse(rang[1]));
             


                    return Json(model.ToArray(), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }

        [HttpPost]
        public ActionResult GetChart(string Id, string range)
        {
            if (Session[CodPrs] != null)
            {

                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    String value = range;
                    Char delimiter = '&';
                    String[] rang = value.Split(delimiter);

                    var model = _ActividadDO.ObtenerPorcentaAct(DateTime.Parse(rang[0]), DateTime.Parse(rang[1]));


                    return Json(model.ToArray(), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }

        [HttpPost]
        public ActionResult GetCharYear(string Id, string range)
        {
            if (Session[CodPrs] != null)
            {

                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    String value = range;
                    Char delimiter = '&';
                    String[] rang = value.Split(delimiter);

                    var model = _ActividadDO.obtenerNumeroXYear(DateTime.Parse(rang[0]), DateTime.Parse(rang[1]));


                    return Json(model.ToArray(), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }

     
        public ActionResult DtTablaDatoDashboard(string id, string range )
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {


                    String value = range;
                    Char delimiter = '&';
                    String[] rang = value.Split(delimiter);
                   
                    bool all = id == "-2" ? true : false;
                    lstatividadRealizada = _ActividadDO.ObtenerActividad(all, int.Parse(id));

                    var lstatividadRealizada1 = (from e in lstatividadRealizada
                                                 where(e.FechaInicio>=DateTime.Parse(rang[0]) && e.FechaInicio<= DateTime.Parse(rang[1]))
                                                 select new
                                                 {
                                                     e.Id,
                                                     IdC = EncParamentro.encriptartexto(e.Id.ToString()),
                                                     e.DescripcionActividad,
                                                     // e.FechaInicio,
                                                     e.ClienteActividad.NombreCompleto,
                                                     e.ClienteActividad.Phone,
                                                     FechaInicio = e.FechaInicio.ToString(),
                                                     FechaFin = e.FechaFin.ToString(),
                                                     TipoActividad = obtenerMC.getActividad(e.TipoActividad),
                                                     e.RespaldoDigital,
                                                     CodigoPersona = EncParamentro.encriptartexto(e.CodigoPersona.ToString()),
                                                     e.CodigoCliente,
                                                     estado = obtenerMC.GetEstado(e.EstadoActividad)
                                                 });

           
                    var rows = lstatividadRealizada1.ToArray();
                    return Json(rows, JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);

                //return View("_TablaDatos");

            }
            return RedirectToAction("LogOff", "LogOn");
        }
        
  

}
}
