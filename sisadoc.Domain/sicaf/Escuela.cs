using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.sicaf
{
  public  class Escuela:Entity
    {
        public virtual int IdEsc { get; set; }
        public virtual int IdUniversidad { get; set; }
        public virtual int IdFacultad { get; set; }
        // Nombre de la Facultad 
        public virtual string NombreEscuela  { get; set; }
        // Nombre de la Tipo de programa donde se encuentra asignado  
        public virtual int Modalidad { get; set; }
        // Clave compuesta con facultad
    //  public virtual Universidad UniversidadEsc { get; set; }
        public virtual Facultad FacultadEsc { get; set; }
    }
}
