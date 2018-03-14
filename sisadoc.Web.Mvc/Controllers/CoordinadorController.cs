using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using sisadoc.Domain.ProcedureClass;
using sisadoc.Web.Mvc.Models;
using sisadoc.Domain.sicaf;
using System.Configuration;
using sisadoc.Tasks.sicaf;
using sisadoc.Infrastructure.sicaf;
namespace sisadoc.Web.Mvc.Controllers
{
    public class CoordinadorController : SisadocController
    {
        #region Declaraciones
        private IList<CarreraDocenteListadoSp> lstcarreraDocenteListadoSP = new List<CarreraDocenteListadoSp>();
        private IList<CarreraCoordinadorSp> lstcarreraCoordinadorSP = new List<CarreraCoordinadorSp>();
        private IList<Universidad> lstuniversidad = new List<Universidad>();
        private IList<Facultad> lstfacultad = new List<Facultad>();
        private IList<Periodo> lstPeriodo = new List<Periodo>();
        private IList<ListModel> lstObs = new List<ListModel>();
         private IList<Escuela> lstescuela = new List<Escuela>();
         private CarreraQry carreraqry = new CarreraQry(new UniversidadRepository(),new FacultadRepository(), new EscuelaRepositoy(), new PeriodoRepository());
        private ActividadDocenteQry _ActividadDO = new ActividadDocenteQry(new ActividadDocenteRepository());
        #endregion 
        //
        // GET: /Coordinador/

        public ActionResult Index()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    lstObs = EventObservacion.CabMensajes(System.Convert.ToInt32(Session[CodPrs]));
                    
                    ViewBag.Msg = lstObs.Count;
                    ViewBag.Smsglst = lstObs;
                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.tipoUsuario = "Coordinador";
                    ViewBag.Menu = Session[Smenu];
                    return View();
                }
            }
           
            
            

                return RedirectToAction("LogOff", "LogOn");
           
          
        }
        public ActionResult RevisarDocente()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                  
                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.Menu = Session[Smenu];
                    lstObs = EventObservacion.CabMensajes(System.Convert.ToInt32(Session[CodPrs]));
                    ViewBag.tipoUsuario = "Coordinador";
                    ViewBag.Msg = lstObs.Count;
                    ViewBag.Smsglst = lstObs;
                    return View();
                }
            }




            return RedirectToAction("LogOff", "LogOn");


        }
      
        #region Parcial
          [HttpGet]
        public ActionResult _cabecerasCarrerasCoordinador()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    IList<ListModel> LstEscuelaSel = new List<ListModel>();
                    IList<ListModel> LstFacultadSel = new List<ListModel>();
                    IList<ListModel> LstPeriodoSel = new List<ListModel>();
                    IList<ListModel> lstuniversidadE = new List<ListModel>();
                    ListModel vm = new ListModel();

                    lstcarreraCoordinadorSP = personaquery.getCarreraCoordinador(System.Convert.ToInt32(Session[CodPrs]));
                    string sede = @"" + ConfigurationManager.AppSettings["Sede"];
                    lstuniversidad = carreraqry.ObtenerUniverisdad(System.Convert.ToInt32(sede));

                    lstuniversidadE.Add(new ListModel
                    {
                        Text = lstuniversidad.First().NombreUniversidad.ToString(),
                        Id = EncParamentro.encriptartexto(lstuniversidad.First().Id.ToString())
                    });
                    foreach (CarreraCoordinadorSp CarreraDocente in lstcarreraCoordinadorSP)
                    {


                        lstfacultad = carreraqry.ObtenerFacultad(CarreraDocente.Universidad, CarreraDocente.Facultad);
                        lstescuela = carreraqry.ObtenerEscuela(CarreraDocente.Universidad, CarreraDocente.Facultad, CarreraDocente.Escuela);
                     

                        IEnumerable<ListModel> RpLstFacultadSel = from facultadlst in LstFacultadSel
                                                                  where facultadlst.Text == lstfacultad.First().NombreFacultad
                                                                  select facultadlst;
                        int numfac = RpLstFacultadSel.Count();
                        if (numfac < 1)
                        {
                            LstFacultadSel.Add(new ListModel
                            {
                                Text = lstfacultad.First().NombreFacultad,
                                Id = EncParamentro.encriptartexto(lstfacultad.First().Id.ToString())
                            });
                        }
                        lstPeriodo = carreraqry.GetPeriodo(CarreraDocente.Universidad, CarreraDocente.Facultad, CarreraDocente.Escuela);
                       
                        int numper = lstPeriodo.Count();
                        if (numper > 0)
                          {
                            IEnumerable<ListModel> RpLstPeriodoSel = from periodolst in LstPeriodoSel
                                                                     where periodolst.Text == lstPeriodo.First().DescripPeriodo
                                                                     select periodolst;
                            int numper2 = RpLstPeriodoSel.Count();
                              if (numper2 < 1)
                                {
                                    LstPeriodoSel.Add(new ListModel
                                    {
                                        Text =lstPeriodo.First().DescripPeriodo,
                                        Id = EncParamentro.encriptartexto(lstPeriodo.First().Id.ToString())
                                    });
                          
                                }
                         }
                      
                        IEnumerable<ListModel> RpLstEscuelaSel = from Escuelalst in LstEscuelaSel
                                                                 where Escuelalst.Text == lstescuela.First().NombreEscuela
                                                                 select Escuelalst;
                        int numesc = RpLstEscuelaSel.Count();
                        if (numesc < 1 && LstFacultadSel.Count() <= 1)
                        {
                            LstEscuelaSel.Add(new ListModel
                            {
                                Text = lstescuela.First().NombreEscuela,
                                Id = EncParamentro.encriptartexto(lstescuela.First().IdEsc.ToString())
                            });
                        }
                    }

                    if (LstPeriodoSel.Count < 1) {
                        LstPeriodoSel.Add(new ListModel
                        {
                            Text = "No Existe Periodo Para la carrera",
                            Id = "99999"
                        });
                    
                    }
                    Session[Speriodo] = EncParamentro.desencriptartexto( LstPeriodoSel.First().Id);
                    SelectList slUniversidad = new SelectList(lstuniversidadE, "Id", "Text");
                    ViewBag.slUniversidad = slUniversidad;

                    SelectList slFacultad = new SelectList(LstFacultadSel, "Id", "Text");
                    ViewBag.slFacultad = slFacultad;

                    SelectList slEscuela = new SelectList(LstEscuelaSel, "Id", "Text");
                    ViewBag.slEscuela = slEscuela;

                    SelectList slPeriodo = new SelectList(LstPeriodoSel, "Id", "Text");
                    ViewBag.slPeriodo = slPeriodo;

                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.Menu = Session[Smenu];

                    return View("");

                }
            }

            return RedirectToAction("LogOff", "LogOn");

        }

        [HttpGet]
          public ActionResult _ListaDocenteRevision()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    IList<ListModel> lsDocentes = new List<ListModel>();
                var periodo=   Session[Speriodo] == null ? Session[Speriodo] = 0: EncParamentro.desencriptartexto(Session[Speriodo].ToString());
                   lstatividadRealizada= actividadDocenteqry.GetActividadDocentePeriodo(System.Convert.ToInt32(Session[Speriodo].ToString()));


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
                 
                    return View("_ListaDocenteRevision");

                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        /// <summary>
        /// Metodo particial para carga metodos opciones de envio de docente.
        /// </summary>
        /// <param name="periodo"></param>
        /// <param name="persona"></param>
        /// <returns></returns>
        public ActionResult _TablaDatos(string periodo, int persona, string mes)
          {
                    if (Session[CodPrs] != null)
                    {
                        if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                        {
                           lstPersona = personaquery.GetPersonaCod(persona);
                           lstObservaciones = observacionesQry.RecuperarObservacioDocente(persona);
                            mes=EncParamentro.desencriptartexto(mes);
                            if (lstObservaciones.Where(x => x.FechaLectura != null && x.CodigoMesObservacion == mes).Count() > 0) 
                               {
                                   ViewBag.ExistObsj = 1;
                               }
                                   ViewBag.CodDocente = persona;
                                   ViewBag.DocenteCalificar = lstPersona.First().NombreCompleto;
                 
                            return View("_TablaDatos");

                        }
                    }
             return RedirectToAction("LogOff", "LogOn");
          }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult _ObservacionDocente()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
      
                    return View("_ObservacionDocente");

                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        #endregion

        #region Ajax

        /// <summary>
        /// Cargar datos de docentes.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ListaCargarDocente(string periodo , int tipo)
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    var allowedStatus = new[] { 1 };
                    if (tipo == 0)
                        {
                        allowedStatus = new[] { 1, 2, 3 };
                        }
                    IList<ListModel> lsDocentes = new List<ListModel>();
                    int PeriodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                    lstatividadRealizada = actividadDocenteqry.GetActividadDocentePeriodo(PeriodoD);
                    IList<ListModel> lsMess = new List<ListModel>();
                    lsMess = obtenerMC.Meses();
                    var listaMeses = (from u in lstatividadRealizada
                                      where allowedStatus.Contains(u.EstadoActividad)
                                      select new { u.FechaFin.Month, u.FechaFin.Year }).ToList().Distinct();
                    // Mes
                    var varmes = (from RANGOmESS in lsMess
                                  join ames in listaMeses on System.Convert.ToInt32(RANGOmESS.Id) equals ames.Month
                                  select new { Text = RANGOmESS.Text, Id = EncParamentro.encriptartexto(ames.Month + "_" + ames.Year), year = ames.Year }).OrderBy(x => x.year);
                    var lstMes =varmes.ToArray();
                    return Json(lstMes, JsonRequestBehavior.AllowGet);
               
               }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        /// <summary>
        ///  Realiaza Actualización del estado del ser aprobado.
        /// </summary>
        /// <param name="periodo"></param>
        /// <param name="docPers"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AprobarActividad(string periodo, int docPers, string mes)
        {
             if (Session[CodPrs] != null)
            {

                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    bool estadosEnvio = false;
                    IList<ListModel> LstResult = new List<ListModel>();
                    int PeriodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                    mes = EncParamentro.desencriptartexto(mes);
                    estadosEnvio = eventoActividades.EnviarActividadDocente(mes, PeriodoD, docPers, Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), 3);

                    LstResult.Add(new ListModel
                    {
                        Text = estadosEnvio.ToString(),
                        Id = estadosEnvio.ToString()
                    });

                    return Json(LstResult.ToArray(), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ObservacionActividad(ObservacionActividad ModelObserv)
        {
            if (Session[CodPrs] != null)
            {
                string periodo = ModelObserv.Periodo;
                int docPers = ModelObserv.CodigoPersonaDestinatario;
                string mes = ModelObserv.CodigoMesObservacion;
                string msg = ModelObserv.Observacion;
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    string hoy = DateTime.Now.ToString();
                    DateTime enviofecha = Convert.ToDateTime(hoy);
                    bool estadosEnvio = false;
                      int PeriodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                    mes = EncParamentro.desencriptartexto(mes);
                    estadosEnvio = eventoActividades.EnviarActividadDocente(mes, PeriodoD, docPers, Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), 2);
                    EventObservacion.InsertarObservacionesDocente(enviofecha, msg, Session[CodPrs].ToString(), docPers.ToString(), Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), mes);
              

                    return Json(new { valid = estadosEnvio, message = "Success" });
                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        [HttpPost]
        public ActionResult ObtenerDocenteEnvio(string periodo)
        {
            if (Session[CodPrs] != null)
            {

                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    int periodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                    Session[Speriodo] = periodoD;
                    lstcarreraDocenteListadoSP = personaquery.ObtenerCarreraDocenteListado(periodoD,12);
                    var eventList = from e in lstcarreraDocenteListadoSP
                                    select new
                                    {
                                        id = e.Cedula,
                                        title = e.NombreProfesor,

                                    };
                    ViewBag.DocenteLista = lstcarreraDocenteListadoSP;
                    var rows = eventList.ToArray();
                    return Json(rows, JsonRequestBehavior.AllowGet);

                }
            }
         return RedirectToAction("LogOff", "LogOn");
           
        }
        /// <summary>
        /// Recuperar nombre de docente por meses actividades para llenar tabla de envio.
        /// </summary>
        /// <param name="idmesSt"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ObtenerActividadMes(string idmesSt)
        {

            if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
            {
                var allowedStatus = new[] { 0, 2 };
       
           
                idmesSt = EncParamentro.desencriptartexto(idmesSt);
                int idmes = obtenerMC.GetSepararCarracteres(idmesSt, "_", 0);
                lstcarreraDocenteListadoSP = personaquery.ObtenerCarreraDocenteListado(System.Convert.ToInt32(Session[Speriodo]), idmes);
                var eventList = from e in lstcarreraDocenteListadoSP
                        
                                select new
                                {
                                    id = e.CodigoProfesor,
                                    title = e.NombreProfesor
                                  
                                };

                var rows = eventList.ToArray();
                return Json(rows, JsonRequestBehavior.AllowGet);

            }

            return null;
        }

        [HttpPost]
        public JsonResult Atender(string Idcodigo)
        {

            if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
            {
                var allowedStatus = new[] { 0, 2 };



                _ActividadDO.AtEstadoActividadDocente(int.Parse(Idcodigo), Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), 3);
            
                var rows ="Atendido";
                return Json(rows, JsonRequestBehavior.AllowGet);

            }

            return null;
        }
        public ActionResult _DespliegueNovedad()
        {

            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    IList<ListModel> lsMess = new List<ListModel>();
                    lsMess = obtenerMC.Meses();
                    var varmes = from RANGOmESS in lsMess
                                 where System.Convert.ToInt32(RANGOmESS.Id) >= 9
                                 select RANGOmESS;
                    SelectList DpMess = new SelectList(varmes, "Id", "Text");
                    ViewBag.lsMess = DpMess;

                    return View("");

                }
            }
            return RedirectToAction("LogOff", "LogOn");
        }
        public ActionResult DtTablaDatos(string periodo, int persona, string mes)
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                    {
                             int      periodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(periodo));
                             if (persona != 0)
                        {
                            mes = EncParamentro.desencriptartexto(mes);
                            int idmes = obtenerMC.GetSepararCarracteres(mes, "_", 0);

                            lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(persona);
                            lstPersona = personaquery.GetPersonaCod(persona);
                            var lstatividadRealizada1 = (from e in lstatividadRealizada
                                                         where e.CodigoPeriodo == periodoD && e.FechaFin.Month == idmes && e.EstadoActividad!=3
                                                         select new
                                                                  {
                                                                     e.Id,
                                                                     IdC = EncParamentro.encriptartexto(e.Id.ToString()),
                                                                     e.DescripcionActividad,
                                                                    // e.FechaInicio,
                                                                    e.ClienteActividad.NombreCompleto,
                                                                    e.ClienteActividad.Phone,
                                                                     FechaInicio =  e.FechaInicio.ToString(),
                                                                     FechaFin= e.FechaFin.ToString(),
                                                                     TipoActividad = obtenerMC.getActividad(e.TipoActividad),
                                                                     e.RespaldoDigital,
                                                                     CodigoPersona=EncParamentro.encriptartexto(e.CodigoPersona.ToString()),
                                                                     e.CodigoCliente,
                                                                    
                                                                    } );

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
          public ActionResult DescargarArchCoordinador(string Idact, string UsuC)
              {
                  if (Session[CodPrs] != null)
                  {

                      if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                      {
                        int  IdactD =System.Convert.ToInt32( EncParamentro.desencriptartexto(Idact));
                         int  UsuCD = System.Convert.ToInt32(EncParamentro.desencriptartexto(UsuC));
                          IList<ActividadDocente> LstActividSel = new List<ActividadDocente>();
                          lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(UsuCD);
                          var eventList = from u in lstatividadRealizada
                                          where u.Id == IdactD
                                          select new { u.RespaldoDigital };
                          var appSettings = ConfigurationManager.AppSettings;
                          string UploadPath = @"" + appSettings["PathArchivos"] + UsuCD + "\\" + eventList.First().RespaldoDigital;
                          byte[] fileBytes = System.IO.File.ReadAllBytes(UploadPath);
                          string fileName = eventList.First().RespaldoDigital;
                          string tipoArchivo = obtenerMC.GetFile(fileName);
                          return File(fileBytes, tipoArchivo, fileName);
                      }
                  }
                  return RedirectToAction("LogOff", "LogOn");

              }
     /// <summary>
          /// REcuperar todas las observaciones enviadas.
     /// </summary>
     /// <param name="MesEnc"></param>
     /// <param name="docente"></param>
     /// <returns></returns>
          [HttpPost]
          public ActionResult MostrarObsPendiente(string MesEnc , int docente)
          {
              if (Session[CodPrs] != null)
              {
                  if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                  {
                      MesEnc = EncParamentro.desencriptartexto(MesEnc);
                      lstObservaciones = observacionesQry.RecuperarObservacioDocente(docente);
                       var eventList = (from u in lstObservaciones
                                       where u.CodigoMesObservacion == MesEnc 
                                       select u.Observacion);
                      var rows = eventList.ToArray();
                      return Json(rows, JsonRequestBehavior.AllowGet);
                  }
              }
              return RedirectToAction("LogOff", "LogOn");

          }
        #endregion

    }
}
