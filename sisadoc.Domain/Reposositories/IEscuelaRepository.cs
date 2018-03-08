using System.Collections.Generic;
using sisadoc.Domain.sicaf;
using SharpArch.Domain.PersistenceSupport;
namespace sisadoc.Domain.Reposositories
{
    public interface IEscuelaRepository:IRepository<Escuela>
    {
        IList<Escuela> GetEscuela(int IdUni, int IdFac, int IdEsc);
    }
}
