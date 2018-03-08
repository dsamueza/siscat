using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.ProcedureClass
{
  public  class CarreraDocenteListadoSp
    {
        public virtual string Cedula { get; set; }
        public virtual int CodigoProfesor { get; set; }
        public virtual string NombreProfesor { get; set; }
        public virtual int Escuela { get; set; }
        public virtual int Facultad { get; set; }
        public virtual int Universidad { get; set; }
        public virtual string Periodo { get; set; }
        public virtual int PeriodoCodigo { get; set; }
        public virtual DateTime PeriodoInicio { get; set; }
        public virtual DateTime PeriodoFin { get; set; }
   
    }
}
