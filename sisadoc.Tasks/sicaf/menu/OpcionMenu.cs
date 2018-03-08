using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sisadoc.Tasks.menu
{
    public class OpcionMenu : MenuPrincipal
    {
        public OpcionMenu() { }

        public OpcionMenu(string optionId, string texto, bool esenlace, string icono)
        {
            this.OptionId = optionId;
            this.Texto = texto;
            this.EsEnlace = esenlace;
            this.Icono = icono;
        }

        public string OptionId { get; set; }

        public string Texto { get; set; }

        public bool EsEnlace { get; set; }

        public string Icono { get; set; }
    }
}