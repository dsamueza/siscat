using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.ProcedureClass
{
   public class HorasTotalesDocenteSp
    {
        public virtual int PersonaCodigo { get; set; }
        public virtual int CodigoPerido { get; set; }
        public virtual string NombreActividad { get; set; }
        public virtual string HorasTotales { get; set; }
        public virtual int tipo { get; set; }
    }
}
