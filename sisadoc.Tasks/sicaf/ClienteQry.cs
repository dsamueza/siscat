using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.ProcedureClass;
namespace sisadoc.Tasks.sicaf
{
   public class ClienteQry
    {
       private readonly IClienteRepository IclienteRepository;
       public ClienteQry(IClienteRepository iclienteRepository)
        {
            this.IclienteRepository = iclienteRepository;
        }
        /// <summary>
        /// Devuelve todos los tipos de identificación disponibles
        /// </summary>
        /// <returns>Lista delos tipos de identificación</returns>
      
       public IList<Cliente> GetCliente()
        {
            return IclienteRepository.obtenerPersona();
        }
    
        //public void UpdatePrs(Persona sprs)
        //{
        //    IpersonaRepository.GuardaPers(sprs);
        //}
    }
}
