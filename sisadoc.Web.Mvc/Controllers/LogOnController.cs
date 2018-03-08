using sisadoc.Domain.ProcedureClass;
using sisadoc.Domain.sicaf;
using sisadoc.Infrastructure.sicaf;
using sisadoc.Tasks.menu;
using sisadoc.Tasks.Seguridad;
using sisadoc.Tasks.sicaf;
using sisadoc.Web.Mvc.Models;
using sisadoc.Web.Mvc.Seguridad;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace sisadoc.Web.Mvc.Controllers
{
    public class LogOnController : SisadocController
    {
        //PersonaQry personaquery = new PersonaQry(new PersonasRepository());
        MenuQry menuqry = new MenuQry(new OpcionesUsuarioRepository() , new OpcionAplicacionRepository() );
        private  IList<Persona> lstpersona = new List<Persona>();
        private IList<RolPersonaSp> lstinformacionRolDocente= new List<RolPersonaSp>();
        private IList<OpcionAplicacion> lstmenu = new List<OpcionAplicacion>();
      //  PermisoUsuario Autentificacion = new PermisoUsuario();
        //
        // GET: /LogOn/
        #region Variables de clase
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
            base.Initialize(requestContext);
        }

        #endregion 
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LogOnModel usuarios)
        {
          
            if (ModelState.IsValid)
            {

                
                lstpersona = personaquery.getCodigoPersona(usuarios.UserName.ToString().ToLower());

                if (lstpersona.Count > 0)
                {
                    Session[usr_cmb_web] = usuarios.UserName;
                    Session[host_cmb_web] = Request.UserHostAddress;
                          var appSettings = ConfigurationManager.AppSettings;
                          bool Isval = System.Convert.ToBoolean(appSettings["Autent"]);
                    //// Se debe descomentar la linea en caso de que se tenga metodo interno de autentificacion con active directory 
                    //// if (MembershipService.ValidateUser(usuarios.UserName, usuarios.Password))
                          if (Autentificacion.IsPassUsr(lstpersona.First().Id.ToString(), usuarios.UserName, usuarios.Password, Isval))
                          {

                        if (Autentificacion.IsAutentifica(lstpersona.First().Id.ToString(), usuarios.UserName))
                        {
                           
                            Session[nombreDoc] = lstpersona.First().NombreCompleto.ToString();
                            Session[CodPrs] = lstpersona.First().Id;
                            //Creacion de menus 
                            MenuPrincipal menu = PedirMenu(Autentificacion.RolPersona(System.Convert.ToInt32(Session[CodPrs])));
                            Session[Smenu] = menu;
                       
                            if (Autentificacion.RolPersona(System.Convert.ToInt32(Session[CodPrs])) == 2)
                            {
                                Session[StipoUsuario] = 2;
                                ViewBag.tipoUsuario="Docente";

                                return RedirectToAction("Index", "Docente");
                            }
                            if (Autentificacion.RolPersona(System.Convert.ToInt32(Session[CodPrs])) == 1)
                            {
                                Session[StipoUsuario] = 1;
                                ViewBag.tipoUsuario = "Coordinador";
                                return RedirectToAction("Index", "Coordinador");
                            }

                            ViewBag.errorMsg = "No existe el rol asignado a la persona";
                            

                        }
                        else
                        {
                            ViewBag.errorMsg = "No tiene permiso en el sitema";

                        }

                    }
                    else
                    {


                        ViewBag.errorMsg = "Contraseña incorrecta";
                    }


                }
                else { ViewBag.errorMsg = "No existe el usuario en el sistema"; }
                   
                }
                 Session.Clear();
                 return View();
            }

        [HttpGet]
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index", "LogOn");
        }
        #region MetodosPrivados

        private MenuPrincipal PedirMenu(int tpoUsuario)
        {

            if (tpoUsuario > 0)
            {
                MenuPrincipal menu = new MenuPrincipal();
                OpcionMenu cabecera = null;

                var opcionesAplicaciones = menuqry.GetOpcTpoUsr(tpoUsuario);//qry_opc_tpoUsr.GetOpcTpoUsr(si_persona.First().UserTpo.Id);
                //OpcionAplicacionUsuarioTipoRepository.ObtenerOpcionesAplicacionUsuarioTipo(u.UsuarioTipo.Id);

                foreach (OpcionesUsuario opcAplUsrTpo in opcionesAplicaciones)
                {
                    cabecera = menu.AgregarOpcion(opcAplUsrTpo.MenuAplicacion.Identificacion, opcAplUsrTpo.MenuAplicacion.NombreMenu, false, true,opcAplUsrTpo.MenuAplicacion.Iconos);
                    AgregarSubOpciones(opcAplUsrTpo.MenuAplicacion, cabecera);
                }

                return menu;
            }
            return null;
        }
        private void AgregarSubOpciones(OpcionAplicacion oap, OpcionMenu opMenu)
        {
            OpcionMenu opcion = null;
            foreach (OpcionAplicacion opcApl in oap.SubOpciones)
            {
                if (opcApl.SubOpciones.Count > 0)
                {
                    //if (oap.Accion == null && oap.Controlador == null)
                    opcion = opMenu.AgregarOpcion(opcApl.Identificacion, opcApl.NombreMenu, false, true, opcApl.Iconos);
                    AgregarSubOpciones(opcApl, opcion);
                    //else

                }
                else
                    opMenu.AgregarOpcion(opcApl.Identificacion, opcApl.NombreMenu, true, opcApl.Iconos);
            }



        }






        #endregion 
        #region Metodoajax
        public ActionResult UrlMenu(string opcionId)
        {
            Encriptar enc = new Encriptar();
            //lstmenu = menuqry.GetUrlMenu(enc.desencriptartexto(opcionId.ToString()));
            lstmenu = menuqry.GetUrlMenu(opcionId.ToString());
            if (lstmenu.Count > 0) return Content(Url.Action(lstmenu.First().Accion.ToString(), lstmenu.First().Controlador.ToString()));
            else      return Content(Url.Action("Index","LogOn"));
        }
        #endregion 
    }



    }

