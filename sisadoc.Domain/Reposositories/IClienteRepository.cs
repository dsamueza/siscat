using SharpArch.Domain.PersistenceSupport;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.Reposositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IList<Cliente> obtenerPersona();


    }
}
