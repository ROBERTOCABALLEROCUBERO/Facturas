using Facturas.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Facturas.Servicios.Interfaces;
using Facturas.DTO;
using System.IO;

namespace Facturas.Servicios.Implementaciones
{
    public class CrearFactura : ICrearFactura
    {
        public void PrintFactura(FacturasDTO factura)
        {
            // Generar el nombre del archivo PDF
            string nombreArchivo = GenerateFileName(factura);

            // Crear un documento PDF
            Document document = new Document();

            // Crear un flujo de salida para guardar el PDF
            using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create))
            {
                // Inicializar el escritor PDF
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Abrir el documento
                document.Open();

                // Agregar la cabecera de la factura
                AddCabeceraFactura(document, factura);

                // Agregar las líneas de la factura
                AddLineasFactura(document, factura);

                // Cerrar el documento
                document.Close();
            }
        }

        private void AddCabeceraFactura(Document document, FacturasDTO factura)
        {
            // Crear una tabla para la cabecera de la factura
            PdfPTable table = new PdfPTable(3); // 3 columnas
            table.WidthPercentage = 100; // La tabla ocupa el ancho completo de la página

            // Agregar el encabezado de la factura
            PdfPCell headerCell = new PdfPCell(new Phrase("FACTURA DE EMPRESA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20)));
            headerCell.Border = PdfPCell.NO_BORDER; // Sin bordes
            headerCell.Colspan = 3; // Ocupa las 3 columnas de la tabla
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER; // Alineación centrada
            table.AddCell(headerCell);

            // Agregar el número de factura
            table.AddCell(GetCell("Nº de Factura: " + factura.NumeroFactura.ToString()));

            // Agregar el ID/Código de Cliente y el nombre del cliente
            table.AddCell(GetCell("ID/Código de Cliente: " + factura.ClienteID.ToString()));
            table.AddCell(GetCell("Nombre del Cliente: " + factura.Cliente.Nombre));

            // Agregar la fecha
            table.AddCell(GetCell("Fecha: " + factura.Fecha.ToString()));

            // Agregar la tabla al documento
            document.Add(table);
        }

        private void AddLineasFactura(Document document, FacturasDTO factura)
        {
            // Crear una tabla para las líneas de la factura
            PdfPTable table = new PdfPTable(4); // 4 columnas
            table.WidthPercentage = 100; // La tabla ocupa el ancho completo de la página

            // Agregar los encabezados de las columnas
            table.AddCell(GetCell("Concepto"));
            table.AddCell(GetCell("Unidades"));
            table.AddCell(GetCell("Precio"));
            table.AddCell(GetCell("Importe"));

            // Agregar las líneas de la factura
            foreach (var linea in factura.LineasFactura)
            {
                table.AddCell(GetCell(linea.Concepto));
                table.AddCell(GetCell(linea.Unidades.ToString()));
                table.AddCell(GetCell(linea.Precio.ToString()));
                table.AddCell(GetCell(linea.Importe.ToString()));
            }
            // Agregar el total de la factura
            PdfPCell totalCell = GetCell("El total de la factura es: " + factura.TotalFactura.ToString());
            totalCell.Colspan = 4; // Ocupa las 4 columnas de la tabla
            table.AddCell(totalCell);
            document.Add(table);
        }

        private PdfPCell GetCell(string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
            cell.Border = PdfPCell.NO_BORDER; // Sin bordes
            cell.Padding = 5f; // Espacio de 5 puntos alrededor del contenido de la celda
            return cell;
        }

        private string GenerateFileName(FacturasDTO factura)
        {
            string fechaActual = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string nombreArchivo = $"{fechaActual}_ID{factura.ClienteID}_Num{factura.NumeroFactura}.pdf";
            string carpetaDocs = "DOCS/Actuales"; // Carpeta donde se guardarán los archivos PDF
            string rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), carpetaDocs, nombreArchivo);
            return rutaArchivo;
        }
    }
}
