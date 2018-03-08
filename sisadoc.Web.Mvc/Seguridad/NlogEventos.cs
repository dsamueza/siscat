using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sisadoc.Web.Mvc.Seguridad
{
    public class NlogEventos
    {
        public string ObtenerMensajeExcepcionLog(Exception ex, string mensajeSistema, string informacionAdicional)
        {
            string mensaje = "[URL: " + HttpContext.Current.Request.Url.ToString() + "]; \r\n";

            if (!string.IsNullOrEmpty(mensajeSistema))
                mensaje += "[Mensaje Sistema: " + mensajeSistema + "]; \r\n";

            if (!string.IsNullOrEmpty(informacionAdicional))
                mensaje += "[Información Adicional: " + informacionAdicional + "]; \r\n";

            if (ex != null)
            {
                if (ex.GetBaseException() != null && ex.GetBaseException().Message != null && ex.GetBaseException().Message != ex.Message)
                    mensaje += "[Mensaje Base Excepción: " + ex.GetBaseException().Message + "]. \r\n";
                mensaje += "[Mensaje Excepción: " + ex.Message + "]. \r\n";
            }
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            mensaje += "[Datos del Browser, Usuario Agente: " + HttpContext.Current.Request.UserAgent
                + ", Browser: " + browser.Browser
                + ", Id: " + browser.Id
                + ", Versión: " + browser.Version
                + "]; \r\n";

            return mensaje;
        }

    }
}