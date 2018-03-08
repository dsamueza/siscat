using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;


namespace sisadoc.Tasks.sicaf
{
  public  class ObservacionQry
    {

      private readonly IObservacionActividadRepository IObservaciones;
      /// <summary>
      /// Constructor de clase
      /// </summary>
      /// <param name="iObservaciones"></param>
      public ObservacionQry(IObservacionActividadRepository iObservaciones)
        {
            this.IObservaciones = iObservaciones;
      
        }
      /// <summary>
      /// Inserta Obsercaciones de los docentes al ser evaluados
      /// </summary>
      /// <param name="Observacion"></param>
      /// <returns></returns>
      public bool InsertObservacionDocente(ObservacionActividad Observacion)
      {
          return IObservaciones.SaveObservacionDocente(Observacion);
      }
      public bool ActualizarFechaRevision(DateTime FechaRevision, string usr_web, string host_web, string idn, int  persona)
      {
          return IObservaciones.UpdateFechaRevision(FechaRevision, usr_web, host_web, idn,persona);
      }
      public IList<ObservacionActividad> RecuperarObservacioDocente(int CodigoPersona)
      {
          return IObservaciones.GetObservacioDocente(CodigoPersona);
      }
    }
}
