using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc.Async;
using System.Web.Profile;
using System.Web.Routing;
using sisadoc.Tasks.Seguridad;
using System.Web.Mvc;
using sisadoc.Web.Mvc.Seguridad;
using sisadoc.Domain.sicaf;
using sisadoc.Infrastructure.sicaf;
using sisadoc.Tasks.sicaf;
using sisadoc.Web.Mvc.Utility;
using System.Collections.Generic;
using sisadoc.Web.Mvc.Events;
using sisadoc.Infrastructure.sicaf.Observacion;
namespace sisadoc.Web.Mvc.Controllers
{
    public abstract class SisadocController : Controller
    {
        // variables de session
        internal const string usr_cmb_web="usr_cmb_web"; // usuario web conectado
        internal const string host_cmb_web = "host_cmb_web"; // usuario web conectado
        internal const string nombreDoc = "nombreDoc";// Nombre de docente logeado
        internal const string CodPrs = "CodPrs";// Codigo de Persona
        internal const string Smenu = "Smenu";// Sesion de opciones de menu del usuario
        internal const string StipoUsuario = "StipoUsuario";// Tipo de Usuario
        internal const string Speriodo = "Speriodo"; // Periodo Seleccionado
        internal const string Smsg = "Smsg"; // Observaciones realizadas
        internal const string Smsglst = "Smsglst"; // Detalle Observaciones
        internal PermisoUsuario Autentificacion = new PermisoUsuario();
        internal Encriptar EncParamentro = new Encriptar();

        internal PersonaQry personaquery = new PersonaQry(new PersonasRepository());
        internal MetodosCM obtenerMC = new MetodosCM(); //
        internal IList<Persona> lstPersona = new List<Persona>();
        internal IList<ActividadDocente> lstatividadRealizada = new List<ActividadDocente>();
        internal ActividadDocenteQry actividadDocenteqry = new ActividadDocenteQry(new ActividadDocenteRepository());
        internal ObservacionQry observacionesQry = new ObservacionQry(new ObservacionActividadRepository());
        internal EventsActividadDocente eventoActividades = new EventsActividadDocente();
        internal EventObservacion EventObservacion = new EventObservacion();
        internal IList<ObservacionActividad> lstObservaciones = new List<ObservacionActividad>();
        // GET: /Sisadoc/

    public void RECUPER(){
    }

    }
}
