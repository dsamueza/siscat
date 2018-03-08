using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.sicaf
{
    public class Periodo:Entity 
    {

        public virtual int IdEscP { get; set; }
        public virtual int IdUniversidadP { get; set; }
        public virtual int IdFacultadP { get; set; }
        public virtual string DescripPeriodo { get; set; }
        public virtual int PeriodoAbierto { get; set; }
        public virtual int PeriodoPropedeutico { get; set; }
        public virtual DateTime PeriodoInicio { get; set; }
        public virtual DateTime PeriodoFin { get; set; }
    }
}
