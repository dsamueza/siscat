using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.Reposositories;
using NHibernate.Transform;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;

namespace sisadoc.Infrastructure.sicaf
{
   public class OpcionAplicacionRepository : NHibernateRepository<OpcionAplicacion>, IOpcionAplicacionRepository
    {
       public IList<OpcionAplicacion> GetUrlMenu(string Idurl)
       {
               return Session.QueryOver<OpcionAplicacion>()
                          .Where(a => a.Identificacion == Idurl)
                       .List();


       }
    }
}
