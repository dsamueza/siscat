using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.sicaf;
using sisadoc.Infrastructure.sicaf;
using sisadoc.Domain.ProcedureClass;
using sisadoc.Tasks.sicaf;
using System.Web.Mvc;


namespace sisadoc.Tasks.Seguridad
{
   public  class PermisoUsuario
    {
       PersonaQry personaquery = new PersonaQry(new PersonasRepository());
       private IList<RolPersonaSp> lstinformacionRolDocente = new List<RolPersonaSp>();
       private IList<Persona> lstPersona =new List<Persona>();
       private bool isAuten=false;
       private int TipoUsuario = 0;
       public bool IsAutentifica( string Id, string hostWeb)
       {

           lstinformacionRolDocente = personaquery.getRolPersona(System.Convert.ToInt32( Id));
           if (lstinformacionRolDocente.Count > 0) isAuten = true;
           else isAuten = false;
           if (hostWeb != null) isAuten = true;
           else isAuten = false;

           return isAuten;
       }
       public bool IsPassUsr(string Id, string hostWeb, string Pass , bool isval)
       {
           lstPersona = personaquery.getCodigoPersona(hostWeb.ToString());
           if (lstPersona.Count > 0  )
           {
               if( lstPersona.First().LoginUsuario==hostWeb.ToString() && lstPersona.First().PasswordUsuario == Pass)  isAuten = true;
               else if (isval) isAuten = true;
               else isAuten = false;
           }else isAuten = false;
         

           return isAuten;
       }
        public int RolPersona (int Id){
      
        lstinformacionRolDocente = personaquery.getRolPersona( System.Convert.ToInt32( Id));
        if (lstinformacionRolDocente.Count > 0) TipoUsuario = lstinformacionRolDocente.First().CategoriaDocente;
        else TipoUsuario =0;

        return TipoUsuario;
        }
          
    }
}
