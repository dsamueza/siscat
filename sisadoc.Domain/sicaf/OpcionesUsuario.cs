using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SharpArch.Domain.DomainModel;
namespace sisadoc.Domain.sicaf
{
    public class OpcionesUsuario : EntityWithTypedId<Int32>
    {

        /// Get/Set para CodigoOpcionAplicacion  (gh_opc_apl_cod)
        [DomainSignature]
        public virtual int CodigoOpcionAplicacion { get; set; }

        /// Get/Set para CodigoUsuarioTipo  (sa_usr_tpo_cod)
        [DomainSignature]
        public virtual int CodigoUsuarioTipo { get; set; }

        /// Get/Set para UsuarioTipo  (sa_usr_tpo_cod)

        public virtual TipoPermiso UsuarioRol { get; set; }
        /// Get/Set para OpcionAplicacion  (sa_usr_tpo_cod)
        public virtual OpcionAplicacion MenuAplicacion { get; set; }




    }
}
