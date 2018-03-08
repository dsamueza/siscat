using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.sicaf
{
  public  class Facultad:Entity
    {
        public virtual int Id { get; set; }
        public virtual int IdUniversidad { get; set; }
        // Nombre de la Facultad 
        public virtual string NombreFacultad { get; set; }
        // Nombre de la Tipo de programa donde se encuentra asignado  
        public virtual int TipoPrograma { get; set; }
        // Clave compuesta con facultad
        public virtual Universidad UniversidadFac { get; set; }

    }
}
