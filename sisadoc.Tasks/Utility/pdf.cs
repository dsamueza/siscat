using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using sisadoc.Domain.ProcedureClass;
using sisadoc.Domain.sicaf;
using sisadoc.Tasks.sicaf;
using sisadoc.Infrastructure.sicaf;
namespace sisadoc.Tasks.Utility
{
    public class pdf
    {
        private IList<CarreraDocenteSp> lstPersona = new List<CarreraDocenteSp>();
        private IList<ActividadDocente> lstatividadRealizada = new List<ActividadDocente>();
        private IList<Escuela> lstEscuela = new List<Escuela>();
        private ActividadDocenteQry actividadDocenteqry = new ActividadDocenteQry(new ActividadDocenteRepository());
        private PersonaQry personaquery = new PersonaQry(new PersonasRepository());
        private CarreraQry carreraqry = new CarreraQry(new UniversidadRepository(), new FacultadRepository(), new EscuelaRepositoy(), new PeriodoRepository());

        public string CreatePDF(string path, string Spath, string mes, int persona, int periodo, int idmes)
        {

            #region Recuperación de información 

            lstPersona = personaquery.getCarreraDocente(persona);
            lstEscuela = carreraqry.ObtenerEscuela(lstPersona.First().Universidad,lstPersona.First().Facultad,lstPersona.First().Escuela);
            lstatividadRealizada = actividadDocenteqry.ObtenerActividadDocente(persona);

            var lstatividadRealizadaAux = (from e in lstatividadRealizada
                                         where e.CodigoPeriodo == periodo && e.FechaFin.Month == idmes
                                           select e   );

           
            #endregion
            #region variable de estilo

            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            #endregion
            //path="C:\DOC" ;
            string nombreFull = "Reporte Docente_" + mes + ".pdf";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);    
            string pathFull = path + "\\" + "Reporte Docente_" + mes+".pdf";
            System.IO.FileStream fs = new FileStream(pathFull, FileMode.Create);
            Document document = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddAuthor("Universidad Isarael");
            document.AddCreator("Universidad Isarael");
            document.AddKeywords("UTE");
            document.AddSubject("Reporte Actividades Docente");
            document.AddTitle("Reporte Actividades Docente");
            document.AddHeader("Header", "Header Text");
           
            // Aperturar el pdf.
            document.Open();
            #region CuerpoPDF
           // logo
            var logo = iTextSharp.text.Image.GetInstance((Spath + "/Content/img/logo-ute.jpg"));
            logo.Alignment=1;
            logo.ScaleAbsoluteHeight(70);
            logo.ScaleAbsoluteWidth(70);
            document.Add(logo);

            // Cabecera
            PdfPTable table = new PdfPTable(2);
            //actual width of table in points
            table.TotalWidth = 216f;
            table.LockedWidth = true;

            float[] widths = new float[] { 1f, 2f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 1;
            //leave a gap before and after the table
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;
            PdfPCell cell = new PdfPCell(new Phrase("Universidad Isarael", boldFont));
            PdfPCell cell1 = new PdfPCell(new Phrase("Reporte de Actividades Docentes"));
            cell.Colspan = 2;
            cell.Border = 0;
            cell.HorizontalAlignment = 1;
            cell1.Colspan = 2;
            cell1.Border = 0;
            cell1.HorizontalAlignment = 1;
            table.AddCell(cell);
            table.AddCell(cell1);
            table.HorizontalAlignment = 1;
            document.Add(table);
            // tabla datos de Docente
            PdfPTable tbDatos = new PdfPTable(5);
            tbDatos.AddCell(new PdfPCell(new Phrase("Codigo :", boldFont)) { Colspan = 1, Border = 0, HorizontalAlignment = 2 });
            tbDatos.AddCell(new PdfPCell(new Phrase(mes)) { Colspan = 4, Border = 0 });
            tbDatos.AddCell(new PdfPCell(new Phrase("Docente :",boldFont)) { Colspan = 1, Border = 0 , HorizontalAlignment=2});
            tbDatos.AddCell(new PdfPCell(new Phrase(lstPersona.First().NombreProfesor)) { Colspan = 4, Border = 0 });
            tbDatos.AddCell(new PdfPCell(new Phrase("Cédula :", boldFont)) { Colspan = 1, Border = 0, HorizontalAlignment = 2 });
            tbDatos.AddCell(new PdfPCell(new Phrase(lstPersona.First().Cedula)) { Colspan = 4, Border = 0 });
            tbDatos.AddCell(new PdfPCell(new Phrase("Carrera :", boldFont)) { Colspan = 1, Border = 0, HorizontalAlignment = 2 });
            tbDatos.AddCell(new PdfPCell(new Phrase(lstEscuela.First().NombreEscuela)) { Colspan = 4, Border = 0 });

            document.Add(tbDatos);
            document.Add(new Paragraph(" "));
         /// tabla 1 
            PdfPTable t = new PdfPTable(6);
            //Row 1
            t.AddCell(new PdfPCell(new Phrase("Codigo", boldFont)) { HorizontalAlignment=1});
            t.AddCell(new PdfPCell(new Phrase("Descripción",boldFont)) { HorizontalAlignment=1});
            t.AddCell(new PdfPCell(new Phrase("Fecha Inicio", boldFont)) { HorizontalAlignment=1});
            t.AddCell(new PdfPCell(new Phrase("Fecha Fin",boldFont)) { HorizontalAlignment=1});
            t.AddCell(new PdfPCell(new Phrase("Tipo",boldFont)) { HorizontalAlignment=1});
            t.AddCell(new PdfPCell(new Phrase("Envidencia", boldFont)) { HorizontalAlignment = 1 });

             foreach (var aux in lstatividadRealizadaAux) {

                 t.AddCell(new PdfPCell(new Phrase(aux.Id.ToString())) { HorizontalAlignment = 1 });
                 t.AddCell(new PdfPCell(new Phrase(aux.DescripcionActividad)) { HorizontalAlignment = 1 });
                 t.AddCell(new PdfPCell(new Phrase(aux.FechaInicio.ToString())) { HorizontalAlignment = 1 });
                 t.AddCell(new PdfPCell(new Phrase(aux.FechaFin.ToString())) { HorizontalAlignment = 1 });
                 t.AddCell(new PdfPCell(new Phrase(getActividad(aux.TipoActividad))) { HorizontalAlignment = 1 });
                 t.AddCell(new PdfPCell(new Phrase(aux.RespaldoDigital)) { HorizontalAlignment = 1 });
            }
        

                /* EJEMPLOS
                //Row 2 - One regular cell followed by four spanned cells
                t.AddCell("R2C1");
                t.AddCell(new PdfPCell(new Phrase("R2C2-5")) { Colspan = 4 });

                //Row 3 - Four spanned cells followed by one regular cell
                t.AddCell(new PdfPCell(new Phrase("R3C1-4")) { Colspan = 4 });
                t.AddCell("R3C5");*/
                document.Add(t);
            #endregion 
            //   Cerrar el contenido del  pdf.
            document.Close();
    
            writer.Close();
        
            fs.Close();
            return nombreFull;
        }
         string getActividad(int id)
        {
            string color = "";
            switch (id)
            {
                case 1:
                    color = "Gestión";
                    break;
                case 2:
                    color = "Academica";

                    break;
                case 3:
                    color = "Vinculación";
                    break;
            }
            return color;
        }
    }
}
