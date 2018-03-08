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

    public class DocenteController : SisadocController
    {
        //
        // GET: /Docente/
      //  private ActividadDocenteQry actividadDocenteqry = new ActividadDocenteQry(new ActividadDocenteRepository());
    //    private EventsActividadDocente eventoActividades = new EventsActividadDocente();
        private CarreraQry carreraqry = new CarreraQry(new UniversidadRepository(),new FacultadRepository(), new EscuelaRepositoy(), new PeriodoRepository());
        private IList<Universidad> lstuniversidad = new List<Universidad>();
        private IList<Facultad> lstfacultad = new List<Facultad>();
        private IList<Escuela> lstactividadDocente = new List<Escuela>();
        private IList<Periodo> lstPeriodo = new List<Periodo>();
      //  private IList<ActividadDocente> lstatividadRealizada= new List<ActividadDocente>();
        private IList<CarreraDocenteSp> lstcarreraDocenteSP = new List<CarreraDocenteSp>();
        private IList<HorasTotalesDocenteSp> lstHorasTotalSP = new List<HorasTotalesDocenteSp>();
        private IList<ListModel> lstObs = new List<ListModel>();
        private ClienteQry _clienteDAO = new ClienteQry(new ClienteRepository());
        public ActionResult Index()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {

                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.Menu = Session[Smenu];
                    ViewBag.tipoUsuario = "Docente";
                    lstObs =EventObservacion.CabMensajes(System.Convert.ToInt32(Session[CodPrs]));
                    //lstObservaciones = observacionesQry.RecuperarObservacioDocente(System.Convert.ToInt32(Session[CodPrs]));
                    //Session[Smsg] = lstObservaciones.Count;
                    ViewBag.Msg = lstObs.Count;
                    ViewBag.Smsglst = lstObs;
                  return View();
                }
            }


            return RedirectToAction("LogOff", "LogOn");
            
        }
   
        [HttpGet]
        public ActionResult Actividad()
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                
                    ViewBag.nombreDoc = Session[nombreDoc];
                    ViewBag.Menu = Session[Smenu];
                    ViewBag.tipoUsuario = "Docente";
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
        #region Parcial
        [HttpGet]
        public ActionResult _IngresoActividad()
            {
                if (Session[CodPrs] != null)
                {
                    if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                    {
                     
                        return View("");

                    }
                }
            return RedirectToAction("LogOff", "LogOn");

            }

        [HttpGet]
        public ActionResult _cabecerasCarreras()
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

                    lstcarreraDocenteSP = personaquery.getCarreraDocente(System.Convert.ToInt32(Session[CodPrs]));
                    string sede = @"" + ConfigurationManager.AppSettings["Sede"];
                    lstuniversidad = carreraqry.ObtenerUniverisdad(System.Convert.ToInt32(sede));
                   
                    lstuniversidadE.Add(new ListModel
                    {
                        Text = lstuniversidad.First().NombreUniversidad.ToString(),
                        Id = EncParamentro.encriptartexto(lstuniversidad.First().Id.ToString())
                    });
                    foreach (CarreraDocenteSp CarreraDocente in lstcarreraDocenteSP)
                    {


                        lstfacultad = carreraqry.ObtenerFacultad(CarreraDocente.Universidad, CarreraDocente.Facultad);
                        lstactividadDocente = carreraqry.ObtenerEscuela(CarreraDocente.Universidad, CarreraDocente.Facultad, CarreraDocente.Escuela);


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
                        IEnumerable<ListModel> RpLstPeriodoSel = from periodolst in LstPeriodoSel
                                                                 where periodolst.Text == CarreraDocente.Periodo
                                                                 select periodolst;
                        int numper = RpLstPeriodoSel.Count();
                        if (numper < 1)
                        {
                            LstPeriodoSel.Add(new ListModel
                            {
                                Text = CarreraDocente.Periodo,
                                Id = EncParamentro.encriptartexto(CarreraDocente.PeriodoCodigo.ToString())
                            });
                        }
                        Session[Speriodo] = EncParamentro.desencriptartexto(LstPeriodoSel.First().Id);
                        IEnumerable<ListModel> RpLstEscuelaSel = from Escuelalst in LstEscuelaSel
                                                                 where Escuelalst.Text == lstactividadDocente.First().NombreEscuela
                                                                 select Escuelalst;
                        int numesc = RpLstEscuelaSel.Count();
                        if (numesc < 1 && LstFacultadSel.Count() <= 1)
                        {
                            LstEscuelaSel.Add(new ListModel
                            {
                                Text = lstactividadDocente.First().NombreEscuela,
                                Id = EncParamentro.encriptartexto(lstactividadDocente.First().IdEsc.ToString())
                            });
                        }
                    }

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
        public ActionResult _DespliegueActividades()
        
        {
          
             if (Session[CodPrs] != null)
                {
                    if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                    {
                        IList<ListModel> lsMess = new List<ListModel>();
                        lsMess = obtenerMC.Meses();
                        var varmes =  from RANGOmESS in lsMess
                                       where System.Convert.ToInt32( RANGOmESS.Id )>=9
                                       select RANGOmESS;
                        SelectList DpMess = new SelectList(varmes, "Id", "Text");
                        ViewBag.lsMess = DpMess;
        
                        return View("");

                    }
                }
             return RedirectToAction("LogOff", "LogOn");
        }
        #endregion
        #region Ajax
        [HttpPost]

        public ActionResult CargaPeriodo(string codperiodoE)
        {
              if (Session[CodPrs] != null)
                {
                    if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                    {
                        if (codperiodoE != "")
                        {
                            var allowedStatus = new[] { 0, 2 };
                            int codperiodo = System.Convert.ToInt32(EncParamentro.desencriptartexto(codperiodoE));
                            lstatividadRealizada = actividadDocenteqry.HorasRealizadas(System.Convert.ToInt32(Session[CodPrs]), codperiodo);
                            //var listmes = from m in lstatividadRealizada
                            //                 ;
                            IList<ListModel> lsMess = new List<ListModel>();
                            lsMess = obtenerMC.Meses();
                            var listaMeses = (from u in lstatividadRealizada
                                              where allowedStatus.Contains(u.EstadoActividad)
                                              select new { u.FechaFin.Month, u.FechaFin.Year }).ToList().Distinct();

                            var varmes = (from RANGOmESS in lsMess
                                          join ames in listaMeses on System.Convert.ToInt32(RANGOmESS.Id) equals ames.Month
                                          select new { Mes = RANGOmESS.Text, Idmes = EncParamentro.encriptartexto(ames.Month + "_" + ames.Year), year = ames.Year }).OrderBy(x => x.year);
                            var rows = varmes.ToArray();
                            return Json(rows, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
              return RedirectToAction("LogOff", "LogOn");
        }
        public ActionResult DescargarArch(string Idact)
                {
                      if (Session[CodPrs] != null)
                        {
                            if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                            {
                              int   IdactD = System.Convert.ToInt32(EncParamentro.desencriptartexto(Idact));
                                IList<ActividadDocente> LstActividSel = new List<ActividadDocente>();
                                lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(System.Convert.ToInt32(Session[CodPrs]));
                                var eventList = from u in lstatividadRealizada
                                                where u.Id == IdactD
                                                select new { u.RespaldoDigital };
                                var appSettings = ConfigurationManager.AppSettings;
                                string UploadPath = @"" + appSettings["PathArchivos"] + Session[CodPrs].ToString() + "\\" + eventList.First().RespaldoDigital;
                                byte[] fileBytes = System.IO.File.ReadAllBytes(UploadPath);
                                string fileName = eventList.First().RespaldoDigital;
                                string tipoArchivo = obtenerMC.GetFile(fileName);
                                return File(fileBytes, tipoArchivo, fileName);
                            }
                           }
                          return RedirectToAction("LogOff", "LogOn");
              
                }
            [HttpPost]

        public ActionResult HorasDocentes(string codperiodo,string mesSt)
        {   
                if (Session[CodPrs] != null)
                        {
                        if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                            {
                                int codperiodoD = System.Convert.ToInt32(EncParamentro.desencriptartexto(codperiodo.ToString()));
                                mesSt = EncParamentro.desencriptartexto(mesSt);
                                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                                {

                                    int mes = obtenerMC.GetSepararCarracteres(mesSt, "_", 0);
                                    lstHorasTotalSP = actividadDocenteqry.obtenerHorasTotales(System.Convert.ToInt32(Session[CodPrs]), mes, codperiodoD);
           
                                return Json(lstHorasTotalSP.ToArray(), JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
        return RedirectToAction("LogOff", "LogOn");
         }
        public ActionResult ObtenerCarreras(string IdFac)
        {
               if (Session[CodPrs] != null)
                        {
                        if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                            {
                               int IdFacD = System.Convert.ToInt32(EncParamentro.desencriptartexto(IdFac));
                                int Universidad = 1;
                                IList<ListModel> LstEscuelaSel = new List<ListModel>();
                               lstcarreraDocenteSP = personaquery.getCarreraDocente(System.Convert.ToInt32(Session[CodPrs]));
                    
                                IList<CarreraDocenteSp>  CarreraDocentes = new List<CarreraDocenteSp>();

                                IEnumerable<CarreraDocenteSp> CarreraDocente = from u in lstcarreraDocenteSP
                                                                               where u.Facultad == IdFacD
                                                              && u.Universidad == Universidad
                                                              select u;

                                if (CarreraDocente.Count() > 0) {
                                    lstactividadDocente = carreraqry.ObtenerEscuela(CarreraDocente.First().Universidad, CarreraDocente.First().Facultad, CarreraDocente.First().Escuela);
                                }
         
                                LstEscuelaSel.Add(new ListModel
                                {
                                    Text = lstactividadDocente.First().NombreEscuela,
                                    Id = EncParamentro.encriptartexto( lstactividadDocente.First().IdEsc.ToString())
                                });
                                return Json(LstEscuelaSel.ToArray(), JsonRequestBehavior.AllowGet);
                            }   
                           }
               return RedirectToAction("LogOff", "LogOn");
                        }
        

        [HttpPost]

        public ActionResult RecuperarActividad(string IdactE)
        {
                if (Session[CodPrs] != null)
                        {
                        if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                            {
                                int Idact =System.Convert.ToInt32( EncParamentro.desencriptartexto(IdactE));  
                     IList<ActividadDocente> LstActividSel = new List<ActividadDocente>();
                        lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(System.Convert.ToInt32(Session[CodPrs]));


                        var eventList = from u in lstatividadRealizada
                                          where u.Id == Idact
                                        select new { u.Id, u.FechaFin, u.FechaInicio, u.DescripcionActividad, u.RespaldoDigital, u.TipoActividad, Cliente = u.ClienteActividad.NombreCompleto + "(" + u.ClienteActividad.Phone + ")", idCliente =u.CodigoCliente};



                        var rows = eventList.ToArray();
                        return Json(rows, JsonRequestBehavior.AllowGet);
                            }
                        }
                return RedirectToAction("LogOff", "LogOn");

        }



        [HttpPost]

        public ActionResult RecuperarNovedad(string IdactE)
        {
            if (Session[CodPrs] != null)
            {
                if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                {
                    int Idact = System.Convert.ToInt32(IdactE);
                    IList<ActividadDocente> LstActividSel = new List<ActividadDocente>();
                    lstatividadRealizada = actividadDocenteqry.GetOneActividad(Idact);

                   
                    var eventList = from u in lstatividadRealizada
                                    where u.Id == Idact
                                    select new {  u.Id,
                                                  u.FechaFin,
                                                  u.FechaInicio,
                                                  u.DescripcionActividad,
                                                  link = ConverImge(@"" + ConfigurationManager.AppSettings["PathArchivos"] + u.PersonaActividad.Id + "\\" + u.RespaldoDigital),
                                                  u.RespaldoDigital, 
                                                  u.TipoActividad, 
                                                  ClienteNombre = u.ClienteActividad.NombreCompleto
                                                  ,  Phone=u.ClienteActividad.Phone 
                                                  , idCliente = u.CodigoCliente };



                    var rows = eventList.ToArray();
                    return Json(rows, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("LogOff", "LogOn");

        }
        public string ConverImge(string Path)
        {
         
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        public ActionResult ObtenerPeriodo(string IdFac, string IdEsc)
        {
                     if (Session[CodPrs] != null)
                        {
                        if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                            {
                              int   IdFacD = System.Convert.ToInt32(EncParamentro.desencriptartexto(IdFac.ToString()));
                              int IdEscD = System.Convert.ToInt32(EncParamentro.desencriptartexto(IdEsc.ToString()));
                                int Universidad = 1;
                                IList<ListModel> LstPeriodoSel = new List<ListModel>();
                                lstPeriodo = carreraqry.GetPeriodo(Universidad, IdFacD, IdEscD);


                                foreach (Periodo CarreraPeriodo in lstPeriodo)
                                {
                                    LstPeriodoSel.Add(new ListModel
                                    {
                                        Text = CarreraPeriodo.DescripPeriodo,
                                        Id = EncParamentro.encriptartexto( CarreraPeriodo.Id.ToString())
                                    });
                                }

                                Session[Speriodo] = EncParamentro.desencriptartexto(LstPeriodoSel.First().Id);
         
                               return Json(LstPeriodoSel.ToArray(), JsonRequestBehavior.AllowGet);
                            }
                        }
                     return RedirectToAction("LogOff", "LogOn");
        }

        

        [HttpPost]
        public ActionResult GuardarActividad(string Inicio, string Fin, string Descripcion, string tipo, string Periodo, string Url, string idCliente)
        {
            if (Session[CodPrs] != null)
                        {
                        if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                            {
                                Periodo = EncParamentro.desencriptartexto(Periodo);
                            

                                bool isInsert = false;
                                string mess = ""; 
                                 DateTime DtInicio = System.Convert.ToDateTime(Inicio);
                                DateTime DtFin = System.Convert.ToDateTime(Fin);
                                IList<ListModel> LstEscuelaSel = new List<ListModel>();
                                ActividadDocente actividadesIngreso = new ActividadDocente();
                                if (Url == "")
                                {
                                    mess = "URL";
                                }
                                else {
  
                              //  actividadDocenteqry.InsertActividadDocente(actividadesIngreso);
                                    isInsert = eventoActividades.InsertarActividadDocente(DtInicio, DtFin, Descripcion, tipo, Session[CodPrs].ToString(), Periodo, Url, Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), idCliente);
                                 mess = isInsert.ToString();
         
                                }

                                LstEscuelaSel.Add(new ListModel
                                {
                                    Text = mess.ToString(),
                                    Id = mess.ToString()
                                });
          
                                return Json(LstEscuelaSel.ToArray(), JsonRequestBehavior.AllowGet);
                            }
                        }
            return RedirectToAction("LogOff", "LogOn");
        }
        [HttpPost]
        public ActionResult ActualizarActividad(string Inicio, string Fin, string Descripcion, string tipo, string Periodo, string id, string UrlPath, string idCliente)
        {
               if (Session[CodPrs] != null)
                        {
                        if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
                            {
                                Periodo = EncParamentro.desencriptartexto(Periodo);
                                id = EncParamentro.desencriptartexto(id);
                                IList<ListModel> LstEscuelaSel = new List<ListModel>();
                                bool isInsert = false;
                                if (UrlPath == "") {
                                    IList<ActividadDocente> LstActividSel = new List<ActividadDocente>();
                                    lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(System.Convert.ToInt32(Session[CodPrs]));
                                    var eventList = from u in lstatividadRealizada
                                                    where u.Id == System.Convert.ToInt32(id)
                                                    select new { u.RespaldoDigital };
                                    UrlPath = eventList.First().RespaldoDigital;
            
                                }
         
                                if (UrlPath != "") {

                                    var file = new StreamReader(this.HttpContext.Request.InputStream, Encoding.UTF8).ReadToEnd();
                                    DateTime DtInicio = System.Convert.ToDateTime(Inicio);
                                    DateTime DtFin = System.Convert.ToDateTime(Fin);
             
                                    ActividadDocente actividadesIngreso = new ActividadDocente();
                                    //  actividadDocenteqry.InsertActividadDocente(actividadesIngreso);
                                    isInsert = eventoActividades.UpdActividadDocente(DtInicio, DtFin, Descripcion, tipo, Session[CodPrs].ToString(), Periodo, UrlPath, Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), id, idCliente);
                                 }



                                LstEscuelaSel.Add(new ListModel
                                {
                                    Text = isInsert.ToString(),
                                    Id = isInsert.ToString()
                                });

                                return Json(LstEscuelaSel.ToArray(), JsonRequestBehavior.AllowGet);
                            }
                        }
               return RedirectToAction("LogOff", "LogOn");
        }
        public ActionResult EliminarActividad(string id)
        {
            var file = new StreamReader(this.HttpContext.Request.InputStream, Encoding.UTF8).ReadToEnd();
         
            IList<ListModel> LstEscuelaSel = new List<ListModel>();
            ActividadDocente actividadesIngreso = new ActividadDocente();
            //  actividadDocenteqry.InsertActividadDocente(actividadesIngreso);
            bool isInsert = eventoActividades.ElmActividadDocente( Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), EncParamentro.desencriptartexto( id));


            LstEscuelaSel.Add(new ListModel
            {
                Text = isInsert.ToString(),
                Id = isInsert.ToString()
            });

            return Json(LstEscuelaSel.ToArray(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult EnviarActividadMesual(string my, string Periodo)
        {
               string hoy = DateTime.Now.ToString();
                    DateTime enviofecha = Convert.ToDateTime(hoy);
            bool estadosEnvio = false;
            IList<ListModel> LstEscuelaSel = new List<ListModel>();
                     int  PeriodoD = System.Convert.ToInt32( EncParamentro.desencriptartexto(Periodo));
                    my = EncParamentro.desencriptartexto(my);
                    estadosEnvio = eventoActividades.EnviarActividadDocente(my, PeriodoD, System.Convert.ToInt32(Session[CodPrs]), Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), 1);
                    observacionesQry.ActualizarFechaRevision(enviofecha, Session[usr_cmb_web].ToString(), Session[host_cmb_web].ToString(), my, System.Convert.ToInt32(Session[CodPrs]));
            LstEscuelaSel.Add(new ListModel
            {
                Text = estadosEnvio.ToString(),
                Id = estadosEnvio.ToString()
            });

            return Json(LstEscuelaSel.ToArray(), JsonRequestBehavior.AllowGet);
        }
           [HttpPost]
        public ActionResult Upload()
        {
            IList<ListModel> LstArchivos = new List<ListModel>();
        //    bool exitdirectorio = false;
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    string fileName = file.FileName;
                    string UploadPath = @"" + ConfigurationManager.AppSettings["PathArchivos"] + Session[CodPrs].ToString();
                    if (!Directory.Exists(UploadPath))  Directory.CreateDirectory(UploadPath);                                
                if (file.ContentLength == 0)
                    continue;
                if (file.ContentLength > 0)
                    {
                        DateTime localDate = DateTime.Now;
                        string extension = Path.GetExtension(file.FileName);
                        string localDateForma = localDate.ToString("yyyyMMddHHmmssfff");
                        string NameFile = "Actividad" + Session[CodPrs].ToString() + localDateForma + extension;
                        string path = Path.Combine(UploadPath, NameFile);
                        

                    file.SaveAs(path);
                    LstArchivos.Add(new ListModel
                    {
                        Text = NameFile.ToString(),
                        Id = NameFile.ToString()
                    });

                }
            }
            return Json(LstArchivos.ToArray(), JsonRequestBehavior.AllowGet);

        }

         [HttpPost]
           public JsonResult ObtenerActividadMes(string idmesSt) {

               if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
               {
                   var allowedStatus = new[] { 0, 2 };
                   lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(System.Convert.ToInt32(Session[CodPrs]));
                   idmesSt = EncParamentro.desencriptartexto(idmesSt);
                 int idmes = obtenerMC.GetSepararCarracteres(idmesSt, "_", 0);

                var eventList = from e in lstatividadRealizada
                                where e.FechaInicio.Month >= idmes
                                              && e.FechaFin.Month <= idmes
                                              && allowedStatus.Contains(e.EstadoActividad)
                                              && e.CodigoPeriodo == System.Convert.ToInt32(Session[Speriodo])


                                select new
                                {
                                    id = e.Id,
                                    title = e.DescripcionActividad,
                                    startEvent = e.FechaInicio,
                                    endEvent = e.FechaFin,
                                    cliente = e.ClienteActividad.NombreCompleto + "(" + e.ClienteActividad.Phone + ")",
                                       //allDay = false,
                                       tipo = e.TipoActividad
                                   };

                   var rows = eventList.ToArray();
                   return Json(rows, JsonRequestBehavior.AllowGet);

               }

               return null;
           }
       [HttpPost]
         public JsonResult GetEventos(string perido)
           {
        
               if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
               {
                   
                   lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(System.Convert.ToInt32(Session[CodPrs]));

                
                   var eventList = from e in lstatividadRealizada
                                   where e.CodigoPeriodo ==Convert.ToInt32( Session[Speriodo])
                                   select new
                                   {
                                       id = EncParamentro.encriptartexto( e.Id.ToString()),
                                       title = e.ClienteActividad.NombreCompleto+" - "+e.DescripcionActividad,
                                       start = e.FechaInicio.ToString("s"),
                                       end = e.FechaFin.ToString("s"),
                                       //allDay = false,
                                       color = obtenerMC.getColor(e.TipoActividad)
                                   };

                   var rows = eventList.ToArray();
                   return Json(rows, JsonRequestBehavior.AllowGet);
           
               }

               return null;
           }
      /// <summary>
      /// Metodo para saber si existe observaciones en Mes
      /// </summary>
      /// <param name="MesEnc"></param>
      /// <returns></returns>
 
      [HttpPost]
       public ActionResult ObservacionesPendiente(string MesEnc)
       {
           if (Session[CodPrs] != null)
           {
               if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
               {
                   MesEnc = EncParamentro.desencriptartexto(MesEnc);
                   lstObservaciones = observacionesQry.RecuperarObservacioDocente(System.Convert.ToInt32(Session[CodPrs]));
                   IList<ActividadDocente> LstActividSel = new List<ActividadDocente>();
                   lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(System.Convert.ToInt32(Session[CodPrs]));


                   int eventList = (from u in lstObservaciones
                                   where u.CodigoMesObservacion == MesEnc
                                        select u).Count();



                  
                   return Json(eventList, JsonRequestBehavior.AllowGet);
               }
           }
           return RedirectToAction("LogOff", "LogOn");

       }
        /// <summary>
        /// Muestra las observaciones Recibidas por el MEs
        /// </summary>
        /// <param name="MesEnc"></param>
        /// <returns></returns>
      [HttpPost]
      public ActionResult MostrarObsPendiente(string MesEnc)
      {
          if (Session[CodPrs] != null)
          {
              if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
              {
                  MesEnc = EncParamentro.desencriptartexto(MesEnc);
                  lstObservaciones = observacionesQry.RecuperarObservacioDocente(System.Convert.ToInt32(Session[CodPrs]));
                  IList<ActividadDocente> LstActividSel = new List<ActividadDocente>();
                  lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(System.Convert.ToInt32(Session[CodPrs]));


                  var eventList = (from u in lstObservaciones
                                   where u.CodigoMesObservacion == MesEnc && u.FechaLectura == null
                                   select u.Observacion);



            var rows = eventList.ToArray();
                  return Json(rows, JsonRequestBehavior.AllowGet);
              }
          }
          return RedirectToAction("LogOff", "LogOn");

      }
        #endregion
        #region autocomplete
      public ActionResult GetClienteArray(string id)
      {
          if (Session[CodPrs] != null)
          {
              if (Autentificacion.IsAutentifica(Session[CodPrs].ToString(), Session[usr_cmb_web].ToString()))
              {
               var cliente=   _clienteDAO.GetCliente();


               var eventList = (from u in cliente

                                select new { data=u.Id, value=u.NombreCompleto+"("+u.Phone+")" });



                  var rows = eventList.ToArray();
                  return Json(rows, JsonRequestBehavior.AllowGet);
              }
          }
          return RedirectToAction("LogOff", "LogOn");

      }
        #endregion 

    }
}
