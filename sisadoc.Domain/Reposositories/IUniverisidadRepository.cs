using SharpArch.Domain.PersistenceSupport;
using sisadoc.Domain.sicaf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.Reposositories
{
    public interface IUniverisidadRepository :IRepository<Universidad>
    {
        IList<Universidad> GetUniversidad(int Id);
    }
}
