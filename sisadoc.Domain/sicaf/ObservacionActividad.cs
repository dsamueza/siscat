using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace sisadoc.Domain.sicaf
{
    public class ObservacionActividad : Entity
    {
      public virtual string Observacion {get;set;}
      public virtual DateTime FechaEnvio {get;set;}
      public virtual int CodigoPersonaEnvio { get; set; }
      public virtual int CodigoPersonaDestinatario { get; set; }
      public virtual Nullable<DateTime> FechaLectura { get; set; }
      public virtual string CodigoMesObservacion { get; set; }
      public virtual string Periodo { get; set; }
      #region variableAudt
      public virtual string usr_hos_web { get; set; }
      public virtual string usr_cmb_web { get; set; }
      #endregion 
    }
}
