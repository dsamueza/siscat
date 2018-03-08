using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.sicaf
{
   public class TipoPermiso : Entity
    {
         public virtual string NombreRol { get; set; }
        
        public virtual IList<OpcionesUsuario> OpcionesAplicacion { get; protected set; }

        public TipoPermiso()
        {
            InitMembers();
        }
        private void InitMembers()
        {
            OpcionesAplicacion = new List<OpcionesUsuario>();
           
            
        }
    }
}
