using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Tasks.menu
{
    public class MenuPrincipal
    {
        public MenuPrincipal()
        {
            OpcionesMenu = new List<OpcionMenu>();
        }

        public List<OpcionMenu> OpcionesMenu { get; set; }

        //public void AgregarOpcion(OpcionMenu opcionMenu)
        //{
        //    OpcionesMenu.Add(opcionMenu);
        //}

        public void AgregarOpcion(string optionid, string texto, bool esenlace, string icono)
        {
            OpcionesMenu.Add(new OpcionMenu { OptionId = optionid, Texto = texto, EsEnlace = esenlace, Icono = icono });
        }

        public OpcionMenu AgregarOpcion(string optionId, string texto, bool EsEnlace, bool retornarOpcion, string icono)
        {
            OpcionMenu opcionMenu = new OpcionMenu(optionId, texto, EsEnlace, icono);
            OpcionesMenu.Add(opcionMenu);
            return (retornarOpcion) ? opcionMenu : null;
        }
    }
}
