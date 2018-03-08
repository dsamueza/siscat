using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.sicaf;
using SharpArch.Domain.PersistenceSupport;
namespace sisadoc.Domain.Reposositories
{
    public interface IOpcionAplicacionRepository : IRepository<OpcionAplicacion>
    {
        IList<OpcionAplicacion> GetUrlMenu(string Idurl);
    }
}
