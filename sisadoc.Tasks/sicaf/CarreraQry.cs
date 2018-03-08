using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
namespace sisadoc.Tasks.sicaf
{
  public  class CarreraQry
    {
      private readonly IUniverisidadRepository Iuniveridad;
      private readonly IFacultadRepository Ifacultad;
      private readonly IEscuelaRepository Iescuela;
      private readonly IPeriodoRepository Iperiodo;
      public CarreraQry(IUniverisidadRepository iuniveridad, IFacultadRepository ifacultad, IEscuelaRepository iescuela, IPeriodoRepository iperiodo)
        {
            this.Iuniveridad = iuniveridad;
            this.Ifacultad = ifacultad;
            this.Iescuela = iescuela;
            this.Iperiodo = iperiodo;
        }
      public IList<Universidad> ObtenerUniverisdad(int IdUniv)
      {
          return Iuniveridad.GetUniversidad(IdUniv);
      }

      public IList<Facultad> ObtenerFacultad(int IdUniv, int IdFac)
      {
          return Ifacultad.GetFacultad(IdUniv, IdFac);
      }
      public IList<Escuela> ObtenerEscuela(int IdUniv, int IdFac,int IdEsc)
      {
          return Iescuela.GetEscuela(IdUniv, IdFac, IdEsc);
      }
      public IList<Periodo> GetPeriodo(int IdUniv, int IdFac, int IdEsc)
      {
          return Iperiodo.GetPeriodo(IdUniv, IdFac, IdEsc);
      }
    }
}
