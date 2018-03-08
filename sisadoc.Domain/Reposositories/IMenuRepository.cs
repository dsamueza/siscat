using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.sicaf;
using SharpArch.Domain.PersistenceSupport;
namespace sisadoc.Domain.Reposositories
{
    public interface IMenuRepository : IRepository<OpcionesUsuario>
    {
        IList<OpcionesUsuario> GetOpcTpoUsr(int usrtpo);
       
    }
}
