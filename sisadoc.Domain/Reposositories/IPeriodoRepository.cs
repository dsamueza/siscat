using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using sisadoc.Domain.sicaf;
using SharpArch.Domain.PersistenceSupport;
using System;


namespace sisadoc.Domain.Reposositories
{
    public interface IPeriodoRepository:IRepository<Periodo>
    {
        IList<Periodo> GetPeriodo( int iduni, int idfac,int idesc );
    }
}
