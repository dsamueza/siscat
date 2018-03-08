using SharpArch.NHibernate;
using System.Collections.Generic;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using NHibernate.Transform;
namespace sisadoc.Infrastructure.sicaf
{
   public class PersonasRepository:NHibernateRepository<Persona>, IPersonaRepository
    {

 
        public PersonasRepository() { }
        public IList<Persona> obtenerPersona(string si_prs_lgn)
        {
            return Session.QueryOver<Persona>()
                            .Where(a => a.LoginUsuario == si_prs_lgn)
                         .List();

        }
        public IList<Persona> obtenerPersonaCod(int codPersona)
        {
            return Session.QueryOver<Persona>()
                            .Where(a => a.Id==codPersona)
                         .List();

        }
        public IList<RolPersonaSp> obtenerRolPersona(int CodPersona)
        {

            IList<RolPersonaSp> resultsp = new List<RolPersonaSp>();
           var res = Session.GetNamedQuery("sp_rol_persona_escuela")
                      .SetInt32("codPersonas", CodPersona)
                          .SetResultTransformer(new AliasToBeanResultTransformer(typeof(RolPersonaSp)));


           //if (res.Count > 0) {
       
           //    resultsp.Add(new RolPersonaSp { NivelPersona = Convert.ToInt32(res [01].ToString()), UnidadOrganizacion = Convert.ToInt32(res) });
           
           //}


           return res.List<RolPersonaSp>();

        }
        public IList<CarreraDocenteSp> ObtenerCarreraDocente(int CodPersona)
        {

            IList<CarreraDocenteSp> resultsp = new List<CarreraDocenteSp>();
            var res = Session.GetNamedQuery("sp_carreraxdocente")
                       .SetInt32("prscods", CodPersona)
                           .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CarreraDocenteSp)));


            //if (res.Count > 0) {

            //    resultsp.Add(new RolPersonaSp { NivelPersona = Convert.ToInt32(res [01].ToString()), UnidadOrganizacion = Convert.ToInt32(res) });

            //}


            return res.List<CarreraDocenteSp>();

        }
        public IList<CarreraCoordinadorSp> ObtenerCarreraCoordinador(int CodPersona)
        {

            IList<CarreraCoordinadorSp> resultsp = new List<CarreraCoordinadorSp>();
            var res = Session.GetNamedQuery("sp_carreraxcoordinador")
                       .SetInt32("prscods", CodPersona)
                           .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CarreraCoordinadorSp)));


            //if (res.Count > 0) {

            //    resultsp.Add(new RolPersonaSp { NivelPersona = Convert.ToInt32(res [01].ToString()), UnidadOrganizacion = Convert.ToInt32(res) });

            //}


            return res.List<CarreraCoordinadorSp>();

        }
  
        public IList<CarreraDocenteListadoSp> ObtenerCarreraDocenteListado(int CodPeriodo , int mes)
        {

            IList<CarreraDocenteListadoSp> resultsp = new List<CarreraDocenteListadoSp>();
            var res = Session.GetNamedQuery("sp_carreraxdocenteListado")
                       .SetInt32("percods", CodPeriodo)
                       .SetInt32("mess", mes)
                           .SetResultTransformer(new AliasToBeanResultTransformer(typeof(CarreraDocenteListadoSp)));


            //if (res.Count > 0) {

            //    resultsp.Add(new RolPersonaSp { NivelPersona = Convert.ToInt32(res [01].ToString()), UnidadOrganizacion = Convert.ToInt32(res) });

            //}


            return res.List<CarreraDocenteListadoSp>();

        }
    }


}
