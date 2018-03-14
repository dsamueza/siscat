using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.ProcedureClass
{
   public class ActividadYear
    {
        public virtual int   Number { get; set; }
        public virtual int Months { get; set; }

        public virtual int years { get; set; }
        public virtual int TIPO { get; set; }
    }
}
