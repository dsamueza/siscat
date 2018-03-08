using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace sisadoc.Domain.sicaf
{
   public  class Persona:Entity
    {

       public virtual string NombreCompleto { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual string LoginUsuario { get; set; }
        public virtual string PasswordUsuario { get; set; }


        #region variableAudt
        public virtual string usr_hos_web { get; set; }
        public virtual string usr_cmb_web { get; set; }
        #endregion 
    }
}
