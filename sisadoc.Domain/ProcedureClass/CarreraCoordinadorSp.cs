using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.ProcedureClass
{
   public class CarreraCoordinadorSp
    {
  
        public virtual int CodigoPersona { get; set; }
        public virtual string NombreProfesor { get; set; }
        public virtual string Cedula { get; set; }
        public virtual int Escuela { get; set; }
        public virtual int Facultad { get; set; }
        public virtual int Universidad { get; set; }
        public virtual int TipoFacultad { get; set; }

    }
}
