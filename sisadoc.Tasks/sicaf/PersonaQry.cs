using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.ProcedureClass;
namespace sisadoc.Tasks.sicaf
{
   public class PersonaQry
    {
       private readonly IPersonaRepository IpersonaRepository;
       public PersonaQry(IPersonaRepository ipersonaRepositories)
        {
            this.IpersonaRepository = ipersonaRepositories;
        }
        /// <summary>
        /// Devuelve todos los tipos de identificación disponibles
        /// </summary>
        /// <returns>Lista delos tipos de identificación</returns>
        public IList<Persona> getCodigoPersona(string si_prs_lgn)
        {
            return IpersonaRepository.obtenerPersona(si_prs_lgn);
        }
       /// <summary>
       /// Recupera la información de la persona por el codigo de persona
       /// </summary>
       /// <param name="codPersona"></param>
       /// <returns></returns>
        public IList<Persona> GetPersonaCod(int codPersona) {
            return IpersonaRepository.obtenerPersonaCod(codPersona);
        
        }
        public IList<RolPersonaSp> getRolPersona(int CodPersona)
        {
            return IpersonaRepository.obtenerRolPersona(CodPersona);
        }
        public IList<CarreraDocenteSp> getCarreraDocente(int CodPersona)
        {
            return IpersonaRepository.ObtenerCarreraDocente(CodPersona);
        }
       /// <summary>
       /// Devuelve el listado de docentes que pertenece al profesor
       /// </summary>
       /// <param name="CodPeriodo"></param>
       /// <returns></returns>
        public IList<CarreraDocenteListadoSp> ObtenerCarreraDocenteListado(int CodPeriodo, int mes) {
            return IpersonaRepository.ObtenerCarreraDocenteListado(CodPeriodo, mes);
        }
       /// <summary>
       /// Devuelve los coordinadores por carrera 
       /// </summary>
       /// <param name="CodPersona"></param>
       /// <returns></returns>
       public IList<CarreraCoordinadorSp> getCarreraCoordinador(int CodPersona)
        {
            return IpersonaRepository.ObtenerCarreraCoordinador(CodPersona);
        }
    
        //public void UpdatePrs(Persona sprs)
        //{
        //    IpersonaRepository.GuardaPers(sprs);
        //}
    }
}
