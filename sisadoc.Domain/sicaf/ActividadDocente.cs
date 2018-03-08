using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.sicaf
{
 public   class ActividadDocente:Entity
    {

        public virtual int Id { get; set; }
        public virtual int CodigoPersona { get; set; }
        public virtual int CodigoPeriodo { get; set; }
        public virtual string DescripcionActividad { get; set; }
        public virtual int TipoActividad { get; set; }
        public virtual string RespaldoDigital { get; set; }
        public virtual Persona PersonaActividad { get; set; }
        public virtual int CodigoCliente { get; set; }
        public virtual Cliente ClienteActividad { get; set; }
        public virtual DateTime FechaInicio { get; set; }
        public virtual DateTime FechaFin { get; set; }
        public virtual int EstadoActividad { get; set; }
        #region variableAudt
        public virtual string usr_hos_web { get; set; }
        public virtual string usr_cmb_web { get; set; }
        #endregion 
    }
}
