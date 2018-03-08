using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sisadoc.Tasks.menu;

using System.Web.Mvc;
using System.Text;
using sisadoc.Web.Mvc.Seguridad;

namespace sisadoc.Web.Mvc.Helpers
{
    /// <summary>
    /// Extensiones para Html
    /// </summary>
    public static class HtmlHelpers
    {

        /// <summary>
        /// <Crea el menú principal>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="menuPrincipal"></param>
        /// <returns></returns>
        public static string CrearMenu(this HtmlHelper html, MenuPrincipal menu)
        {
         try{
                if (menu.OpcionesMenu.Count > 0)
                {
                    StringBuilder sbMenu = new StringBuilder();
                    menu.OpcionesMenu.ForEach(opcion => sbMenu.AgregarSubLista(opcion));
                    return sbMenu.ToString();
                }
                else
                {
                    return "No hay opciones de menú creadas";
                }
         }
         catch (Exception e) { return "No hay opciones de menú creadas" +e.ToString(); }
           
                
         
          
        }

        /// <summary>
        /// Crea las opciones 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="opcionMenu"></param>
        private static void AgregarSubLista(this StringBuilder stringBuilder, OpcionMenu opcionMenu)
        {

            Encriptar enc = new Encriptar();

            
            if (opcionMenu.EsEnlace)
            {
     
            
               //// Crea los enlaces de las paginas
                string Icoli = "";//@"<span class=""glyphicon glyphicon-hand-right""></span>";
                stringBuilder.AgregarCodigo(string.Format("<li><a href=\"javascript:DirMenu({0})\" >{1} {2}</a>", "'" + enc.encriptartexto( opcionMenu.OptionId )+ "'", opcionMenu.Texto, Icoli));
            }
            else
            {


                //// Crea las cabeceras de la paginas
                stringBuilder.AgregarCodigo(string.Format(@"<li  data-toggle=""collapse""  class=""collapsed"" data-target=""#{0}"" "">", opcionMenu.Texto));

                stringBuilder.AgregarCodigo(string.Format(@"<a href=""#""> <i class=""{0}""></i>", opcionMenu.Icono));
                stringBuilder.AgregarCodigo(string.Format("{0}</a>", opcionMenu.Texto));
            }

            if (opcionMenu.OpcionesMenu.Count > 0)
            {
                //stringBuilder.AgregarCodigo(@"<ul class=""dropdown-menu"">");
                stringBuilder.AgregarCodigo(string.Format(@"<ul class=""sub-menu collapse"" id=""{0}"">", opcionMenu.Texto));
                opcionMenu.OpcionesMenu.ForEach(subopcion => stringBuilder.AgregarSubLista(subopcion));
                stringBuilder.AgregarCodigo("</ul>");
            }
            stringBuilder.AgregarCodigo("</li>");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="texto"></param>
        /// <returns></returns>
        private static StringBuilder AgregarCodigo(this StringBuilder stringBuilder, string texto)
        {
            return stringBuilder.Append(texto);
            //return stringBuilder.AppendLine(texto);
        }


    }

}