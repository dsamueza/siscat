using SharpArch.Domain.PersistenceSupport;
using sisadoc.Domain.sicaf;
using sisadoc.Domain.ProcedureClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Domain.Reposositories
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        IList<Persona> obtenerPersona(string loginUsuario);
        IList<Persona> obtenerPersonaCod(int codPersona);
        IList<RolPersonaSp> obtenerRolPersona(int CodPersona);
        IList<CarreraDocenteSp> ObtenerCarreraDocente(int CodPersona);
        IList<CarreraDocenteListadoSp> ObtenerCarreraDocenteListado(int CodPeriodo , int mes);
        IList<CarreraCoordinadorSp> ObtenerCarreraCoordinador(int CodPersona);

    }
}
