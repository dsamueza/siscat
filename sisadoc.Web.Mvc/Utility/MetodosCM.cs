using sisadoc.Web.Mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace sisadoc.Web.Mvc.Utility
{
    public class MetodosCM
    {
        public string getColor(int id)
        {
            string color = "No Color";
            switch (id)
            {
                case 1:
                    color = "#378006";
                    break;
                case 2:
                    color = "#067D80";
                    break;
                case 3:
                    color = "#800606";
                    break;
            }
            return color;

        }
        public IList<ListModel> Meses()
        {
            string[,] mtMes = new string[,] { { "Enero", "1" }, { "Febrero", "2" }, { "Marzo", "3" }, 
                                                { "Abril", "4" }, { "Mayo", "5" }, { "Junio", "6" }, 
                                                { "Julio", "7" }, { "Agosto", "8" },  { "Septiembre", "9" },
                                                 { "Octubre", "10" },{ "Noviembre", "11" },{ "Diciembre", "12" }};
            IList<ListModel> mes = new List<ListModel>();
            for (int i = 0; i < 12; i++)
            {
                mes.Add(new ListModel
                {

                    Id = mtMes[i, 1],
                    Text = mtMes[i, 0]

                });

            }

            return mes;

        }
        public string GetFile(string path)
        {
            string extension = new FileInfo(path).Extension;
            string extesionApp = "";

            if (extension != null || extension != string.Empty)
            {
                switch (extension)
                {
                    case ".pdf":
                        extesionApp = "application/pdf";
                        break;
                    case ".txt":
                        extesionApp = "application/plain";
                        break;
                    case ".jpeg":
                        extesionApp = "application/jpeg";
                        break;
                    case ".doc":
                        extesionApp = "application/msword";
                        break;
                    case ".docx":
                        extesionApp = "application/msword";
                        break;
                    case ".xls":
                        extesionApp = "application/msexcel";
                        break;
                    case ".xlsx":
                        extesionApp = "application/msexcel";
                        break;
                    default:
                        extesionApp = "application/octet-stream";
                        break;
                }
            }

            return extesionApp;
        }
        public int GetSepararCarracteres(string Palabra, string separador, int Numpalabra)
        {
            String[] elements;
            String pattern = separador;
            if (Palabra != null)
            {
                elements = Regex.Split(Palabra, pattern);
                return System.Convert.ToInt32(elements[Numpalabra]);
            }
            return -1;

        }
        public string getActividad(int id)
        {
            string color = "";
            switch (id)
            {
                case 1:
                    color = "Gestión";
                    break;
                case 2:
                    color = "Pedidos";

                    break;
                case 3:
                    color = "Preguntas";
                    break;
            }
            return color;
        }

        public string GetEstado(int id)
        {
            string color = "";
            switch (id)
            {
                case 1:
                    color = "Pendiente";
                    break;
                case 2:
                    color = "Pedidos";

                    break;
                case 3:
                    color = "Atendida";
                    break;
            }
            return color;
        }
    }
}