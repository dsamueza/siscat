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
    public class OpcionesUsuarioRepository : NHibernateRepository<OpcionesUsuario>, IMenuRepository
    {
        public IList<OpcionesUsuario> GetOpcTpoUsr(int usrtpo)
        {
            //StringBuilder sb = new StringBuilder("from OpcionesUsuario oap where oap.UsuarioTipo.Id = :usrtpo and oap.OpcionAplicacion.OpcionPadre is null");
            //sb.Append(" order by oap.OpcionAplicacion.Identificacion");
            //return Session.CreateQuery(sb.ToString()).SetInt32("usrtpo", usrtpo).List<OpcionesUsuario>();
            ////return  Session.QueryOver<opc_apl_usr_tipo>()
            ////             .Where(a => a.UsuarioTipo.Id == usrtpo)
            ////             .And(a => a.OpcionAplicacion.OpcionPadre != null)
            ////             .OrderBy(a => a.OpcionAplicacion.Identificacion).Asc
            ////          .List();

            return Session.QueryOver<OpcionesUsuario>()
                           .Where(a => a.UsuarioRol.Id == usrtpo)
                        .List();


        }
        //public IList<OpcionAplicacion> GetOpcTpoUsr(string Idurl)
        //{
        //    //StringBuilder sb = new StringBuilder("from OpcionesUsuario oap where oap.UsuarioTipo.Id = :usrtpo and oap.OpcionAplicacion.OpcionPadre is null");
        //    //sb.Append(" order by oap.OpcionAplicacion.Identificacion");
        //    //return Session.CreateQuery(sb.ToString()).SetInt32("usrtpo", usrtpo).List<OpcionesUsuario>();
        //    ////return  Session.QueryOver<opc_apl_usr_tipo>()
        //    ////             .Where(a => a.UsuarioTipo.Id == usrtpo)
        //    ////             .And(a => a.OpcionAplicacion.OpcionPadre != null)
        //    ////             .OrderBy(a => a.OpcionAplicacion.Identificacion).Asc
        //    ////          .List();

        //    return Session.QueryOver<OpcionAplicacion>()
        //                   .Where(a => a.Identificacion == Idurl)
        //                .List();


        //}

    }
}
