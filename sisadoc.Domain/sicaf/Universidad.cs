using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.sicaf
{
  public  class Universidad:Entity
    {
        // Nombre de la univeridad 
        public virtual string NombreUniversidad { get; set; }
        // Sede donde se encuentra
        public virtual string NombreSede { get; set; }
        // Estado de validez
        public virtual int Estado { get; set; }
 
    
    }
}
