using sisadoc.Domain.sicaf;
using sisadoc.Infrastructure.sicaf;
using sisadoc.Tasks.sicaf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sisadoc.Web.Mvc.Utility;
using sisadoc.Infrastructure.sicaf.Observacion;
using sisadoc.Web.Mvc.Models;

namespace sisadoc.Web.Mvc.Events
{
    public class EventObservacion
    {
        private  IList<ObservacionActividad> lstObservaciones = new List<ObservacionActividad>();
        private PersonaQry personaquery = new PersonaQry(new PersonasRepository());
        private IList<Persona> lstPersona = new List<Persona>();
        internal ObservacionQry observacionesQryE = new ObservacionQry(new ObservacionActividadRepository());
        public MetodosCM MC = new MetodosCM();
        ObservacionActividad Observaciones = new ObservacionActividad();
        MetodosCM obtenerCM = new MetodosCM();
        public bool InsertarObservacionesDocente(DateTime FechaEnvio, string Mensaje, string codprsE, string codprsD, string usr_cmb_web, string host_cmb_web, string mes)
        {
            bool IsInsert = false;

        
                Observaciones.FechaEnvio = FechaEnvio;
                Observaciones.Observacion= Mensaje;
                Observaciones.CodigoPersonaEnvio = System.Convert.ToInt32(codprsE);
                Observaciones.CodigoPersonaDestinatario = System.Convert.ToInt32(codprsD);
            ///   Observaciones.FechaLectura=   DateTime.Today;
                Observaciones.usr_cmb_web = usr_cmb_web;
                Observaciones.usr_hos_web = host_cmb_web;
                Observaciones.CodigoMesObservacion =mes;
            observacionesQryE.InsertObservacionDocente(Observaciones);
            

                IsInsert = true;
        


            return IsInsert;
        }
        public IList<ListModel> CabMensajes(int codigoPersona)
        {
            IList<ListModel> lstMensaje =new List<ListModel>();

            lstObservaciones = observacionesQryE.RecuperarObservacioDocente(codigoPersona);
            int NumeroObservacion= lstObservaciones.Count;
            IList<ListModel> lsMess = new List<ListModel>();
            lsMess = MC.Meses();
            foreach(var Obs in lstObservaciones.Where(x => x.FechaLectura == null) ){
              lstPersona=  personaquery.GetPersonaCod(Obs.CodigoPersonaEnvio);
              var mes = (from u in lsMess
                         where u.Id == obtenerCM.GetSepararCarracteres(Obs.CodigoMesObservacion, "_", 0).ToString()
                         select new { u.Text }).Single().Text;


                lstMensaje.Add(new ListModel
                {
                    Text = "Tiene observaciones enviadas por " + lstPersona.First().NombreCompleto + " de las activades realizadas en el mes " + mes + " del " + obtenerCM.GetSepararCarracteres(Obs.CodigoMesObservacion, "_", 1).ToString(),
                    Id = NumeroObservacion.ToString()
                });

            }
            
            return lstMensaje;

        }
    }
}