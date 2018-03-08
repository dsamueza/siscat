using sisadoc.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sisadoc.Domain.ProcedureClass;
using sisadoc.Tasks.Utility;
using System.Configuration;
namespace sisadoc.Web.Mvc.Controllers
{
    public class ReporteDocenteController : SisadocController
    {
        //        private IList<ListModel> lstObs = new List<ListModel>();
        // GET: /ReporteDocente/
        private IList<ListModel> lstObs = new List<ListModel>();
        private pdf Pdf = new pdf();
        private GeneradorExcel excel = new GeneradorExcel();
        public ActionResult Index()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {

                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.Menu = Session[Smenu];

                    lstObs = EventObservacion.CabMensajes(System.Convert.ToInt32(Session[CodPrs]));
                    //lstObservaciones = observacionesQry.RecuperarObservacioDocente(System.Convert.ToInt32(Session[CodPrs]));
                    //Session[Smsg] = lstObservaciones.Count;
                    ViewBag.Msg = lstObs.Count;
                    ViewBag.Smsglst = lstObs;
                    return View();
                }
            }


            return RedirectToAction("LogOff", "LogOn");
        }
        /// <summary>
        /// Vista para mostrar reportes de actividades de docentes.
        /// </summary>
        /// <returns></returns>
        public ActionResult ActividadesDocente()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {

                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.Menu = Session[Smenu];

                    lstObs = EventObservacion.CabMensajes(System.Convert.ToInt32(Session[CodPrs]));
                    //lstObservaciones = observacionesQry.RecuperarObservacioDocente(System.Convert.ToInt32(Session[CodPrs]));
                    //Session[Smsg] = lstObservaciones.Count;
                    ViewBag.Msg = lstObs.Count;
                    ViewBag.Smsglst = lstObs;
                    return View();
                }
            }


            return RedirectToAction("LogOff", "LogOn");
        }
        # region Partial
        [HttpGet]
        public ActionResult _ListaDocente()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    IList<ListModel> lsDocentes = new List<ListModel>();
                    if (Session[Speriodo] == null) Session[Speriodo] = 0;
                    lstatividadRealizada = actividadDocenteqry.GetActividadDocentePeriodo(System.Convert.ToInt32(Session[Speriodo]));


                    //        lstatividadRealizada = actividadDocenteqry.HorasRealizadas(System.Convert.ToInt32(Session[CodPrs]), System.Convert.ToInt32(Session[Speriodo]));
                    //var listmes = from m in lstatividadRealizada
                    //                 ;

                    int mes;
                    IList<ListModel> lsMess = new List<ListModel>();
                    lsMess = obtenerMC.Meses();

                    var listaMeses = (from u in lstatividadRealizada
                                      where u.EstadoActividad == 1
                                      select new { u.FechaFin.Month, u.FechaFin.Year }).ToList().Distinct();

                    var varmes = (from RANGOmESS in lsMess
                                  join ames in listaMeses on System.Convert.ToInt32(RANGOmESS.Id) equals ames.Month
                                  select new { Text = RANGOmESS.Text, Id = EncParamentro.encriptartexto(ames.Month + "_" + ames.Year), year = ames.Year }).OrderBy(x => x.year);
                    SelectList DpMess = new SelectList(varmes, "Id", "Text");
                    ViewBag.lsMess = DpMess;

                    return View("_ListaDocente");

                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }

        #endregion
        #region 
        [HttpPost]
        public ActionResult DTReporteDocente(string periodo, int persona, string mes)
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    int periodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                    if (persona != 0)
                    {
                        mes = EncParamentro.desencriptartexto(mes);
                        int idmes = obtenerMC.GetSepararCarracteres(mes, "_", 0);

                        lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(persona);
                        lstPersona = personaquery.GetPersonaCod(persona);
                        var lstatividadRealizada1 = (from e in lstatividadRealizada
                                                     where e.CodigoPeriodo == periodoD && e.FechaFin.Month == idmes
                                                     select new
                                                     {
                                                         e.Id,
                                                         IdC = EncParamentro.encriptartexto(e.Id.ToString()),
                                                         e.DescripcionActividad,
                                                         // e.FechaInicio,
                                                         FechaInicio = e.FechaInicio.ToString(),
                                                         FechaFin = e.FechaFin.ToString(),
                                                         TipoActividad = obtenerMC.getActividad(e.TipoActividad),
                                                         e.RespaldoDigital,
                                                         CodigoPersona = EncParamentro.encriptartexto(e.CodigoPersona.ToString()),

                                                     });

                        ViewBag.DocenteCalificar = lstPersona.First().NombreCompleto;
                        ViewBag.ActividadesRealizadas = lstatividadRealizada1;
                        var rows = lstatividadRealizada1.ToArray();
                        return Json(rows, JsonRequestBehavior.AllowGet);
                      
                    }

                    return Json("", JsonRequestBehavior.AllowGet);
                    //return View("_TablaDatos");

                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        /// <summary>
        /// Generador de PDF
        /// </summary>
        /// <param name="periodo"></param>
        /// <param name="persona"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public ActionResult DTDescarga(string periodo, int persona, string mes)
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                    {
                    int periodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                    if (persona != 0)
                    {
                        mes = EncParamentro.desencriptartexto(mes);
                          int idmes = obtenerMC.GetSepararCarracteres(mes, "_", 0);
                        string path = @"" + ConfigurationManager.AppSettings["PathArchivos"] + "ReporteDocente\\" + persona;
                        string pdfpath = Server.MapPath("..");
                        string nombreFull = Pdf.CreatePDF(path, pdfpath, mes, persona, periodoD, idmes);
                        byte[] fileBytes = System.IO.File.ReadAllBytes(path + "\\" + nombreFull);
                        string fileName = nombreFull;
                        string tipoArchivo = obtenerMC.GetFile(fileName);
                        return File(fileBytes, tipoArchivo, fileName);
                    }

                    return Json("", JsonRequestBehavior.AllowGet);
                    //return View("_TablaDatos");

                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        /// <summary>
        /// Generado de Excel
        /// </summary>
        /// <param name="periodo"></param>
        /// <param name="persona"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public ActionResult DTDescargaExcel(string periodo, int persona, string mes)
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    int periodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                    if (persona != 0)
                    {
                        mes = EncParamentro.desencriptartexto(mes);
                        int idmes = obtenerMC.GetSepararCarracteres(mes, "_", 0);
                        string path = @"" + ConfigurationManager.AppSettings["PathArchivos"] + "ReporteDocente\\" + persona;
                        string pdfpath = Server.MapPath("..");
                        string nombreFull = excel.CrearExcel(path, pdfpath, mes, persona, periodoD, idmes);
                        byte[] fileBytes = System.IO.File.ReadAllBytes(path + "\\" + nombreFull);
                        string fileName = nombreFull;
                        string tipoArchivo = obtenerMC.GetFile(fileName);
                        return File(fileBytes, tipoArchivo, fileName);
                    }

                    return Json("", JsonRequestBehavior.AllowGet);
                    //return View("_TablaDatos");

                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        #endregion
    }
}
