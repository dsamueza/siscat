using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SharpArch.Domain.DomainModel;

namespace sisadoc.Domain.sicaf
{
    public class OpcionAplicacion : EntityWithTypedId<Int32>
    {

     public OpcionAplicacion()
        {
            SubOpciones = new List<OpcionAplicacion>();            
        }

         /// Get/Set para OpcionPadre  (si__si_opc_apl_cod)
         public virtual OpcionAplicacion OpcionPadre { get; set; }

         /// Get/Set para Identificacion  (si_opc_apl_id)
         public virtual string Identificacion { get; set; }

         /// Get/Set para NombreMenu  (si_opc_apl_nom_menu)
         public virtual string NombreMenu { get; set; }

         /// Get/Set para NombreVentana  (si_opc_apl_nom_ventana)
         public virtual string NombreVentana { get; set; }

         /// Get/Set para Accion  (si_opc_apl_action)
         public virtual string Accion { get; set; }

         /// Get/Set para Controlador  (si_opc_apl_controller)
         public virtual string Controlador { get; set; }

         public virtual string Iconos { get; set; }

        public virtual IList<OpcionAplicacion> SubOpciones { get; set; }

    }
}
