using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicBo;
namespace Utils
{
    public class Util
    {
        PdfPTable table;
        LogicBo.WorkingAtHeightBo _WorkingAtHeightBo;



        public void CreateCurriculum(int EquipementID)
        {
            DataSet dsResultEnc = new DataSet();

            ArrayList arrUbicaciones = new ArrayList();
            ArrayList arrModelo = new ArrayList();
            ArrayList arrFechaFab = new ArrayList();
            ArrayList arrMarca = new ArrayList();
            ArrayList arrSerial = new ArrayList();
            ArrayList arrLote = new ArrayList();
            ArrayList arrComentariost = new ArrayList();
            ArrayList arrRFID = new ArrayList();
            ArrayList arrAprovado = new ArrayList();
            ArrayList arrInspector = new ArrayList();
            ArrayList arrFinal = new ArrayList();
            ArrayList arrFechaInsp = new ArrayList();
            string Sede = "";
            string Equipo = "";
            string RFID = "";
            string CodEq = "";
            int i = 0;
            _WorkingAtHeightBo = new LogicBo.WorkingAtHeightBo();
            dsResultEnc.Tables.Add(_WorkingAtHeightBo.GetDatabyElement(EquipementID));

            Sede = dsResultEnc.Tables[0].Rows[0]["Sede"].ToString();
            Equipo = dsResultEnc.Tables[0].Rows[0]["Equipo"].ToString();
            RFID = dsResultEnc.Tables[0].Rows[0]["Consecutivo"].ToString();
            CodEq = dsResultEnc.Tables[0].Rows[0]["CodE"].ToString();

            string pathPDF = System.Configuration.ConfigurationManager.AppSettings["PathPDFCurriculum"].ToString();


            try
            {
                Document document = new Document(PageSize.LETTER, 40, 40, 40, 40);
                PdfWriter pdfWrite = PdfWriter.GetInstance(document, new FileStream(pathPDF + @"HV_EQUIPOS1\RG03-PL 260_HV_" + CodEq + "_" + RFID + ".pdf", FileMode.OpenOrCreate));
                document.Open();
                table = new PdfPTable(3);
                table.TotalWidth = 550.0F;
                table.LockedWidth = true;
                table.DefaultCell.BorderWidth = 20;
                int[] Columns1 = new int[] { 60, 200, 60 };
                table.SetWidths(Columns1);
                table.SpacingBefore = 1.0F;
                table.SpacingAfter = 1.0F;
                table.DefaultCell.Border = Element.RECTANGLE;
                iTextSharp.text.Font fuenteEncabezado = FontFactory.GetFont("Bookman Old Style", 14, iTextSharp.text.Font.BOLD);


                string strRutaImagen1 = pathPDF + "Content\\Images\\Logo.png";
                iTextSharp.text.Image imagen1;
                //  Creamos la imagen y le ajustamos el tama�o
                imagen1 = iTextSharp.text.Image.GetInstance(strRutaImagen1);
                imagen1.BorderWidth = 0;
                imagen1.Alignment = Element.ALIGN_RIGHT;
                float percentage1 = 0.0F;
                percentage1 = (100 / imagen1.Width);
                imagen1.ScalePercent(percentage1 * 100);

                iTextSharp.text.pdf.PdfPCell celda = new iTextSharp.text.pdf.PdfPCell(imagen1);
                celda.Colspan = 1;
                celda.Rowspan = 1;
                celda.Padding = 5;
                celda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda);



                celda = new iTextSharp.text.pdf.PdfPCell(new Phrase("Hoja de Vida de Equipos para Trabajo en Altura", fuenteEncabezado));
                celda.Colspan = 1;
                celda.Padding = 5;
                celda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda);

                celda = new iTextSharp.text.pdf.PdfPCell(new Phrase("RG03-PL260 Versión 1 05 / 05 / 2017", fuenteEncabezado));
                celda.Colspan = 1;
                celda.Padding = 5;
                celda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda);

                document.Add(table);




                table = new PdfPTable(2);
                table.TotalWidth = 550.0F;
                table.LockedWidth = true;
                table.DefaultCell.BorderWidth = 20;
                Columns1 = new int[] { 200, 300 };
                table.SetWidths(Columns1);
                table.SpacingBefore = 1.0F;
                table.SpacingAfter = 1.0F;
                table.DefaultCell.Border = Element.RECTANGLE;
                fuenteEncabezado = FontFactory.GetFont("Bookman Old Style", 9, iTextSharp.text.Font.BOLD);

                iTextSharp.text.pdf.PdfPCell celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Fecha de creación Hoja de vida:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.Border = Rectangle.NO_BORDER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["FechaCreacion"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Creador de la hoja de vida:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.Border = Rectangle.NO_BORDER;
                table.AddCell(celda1);



                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["Creador"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Nombre:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["Nombre"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Cargo:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.Border = Rectangle.NO_BORDER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["Cargo"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Cc / Asignado:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.Border = Rectangle.NO_BORDER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Información del Equipo", fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                document.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 550.0F;
                table.LockedWidth = true;
                table.DefaultCell.BorderWidth = 20;
                Columns1 = new int[] { 300, 300 };
                table.SetWidths(Columns1);
                table.SpacingBefore = 1.0F;
                table.SpacingAfter = 1.0F;
                table.DefaultCell.Border = Element.RECTANGLE;
                fuenteEncabezado = FontFactory.GetFont("Bookman Old Style", 9, iTextSharp.text.Font.BOLD);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Datos del Equipo", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Fotografias del Equipo", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Consecutivo:" + dsResultEnc.Tables[0].Rows[i]["Consecutivo"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                string[] SplitFotos;
                int intTotalFotos;
                if (!String.IsNullOrEmpty(dsResultEnc.Tables[0].Rows[0]["Fotos"].ToString()))
                {
                    SplitFotos = dsResultEnc.Tables[0].Rows[i]["Fotos"].ToString().Split('/');
                    intTotalFotos = SplitFotos.Length;
                }
                else
                {
                    intTotalFotos = 0;
                }


                iTextSharp.text.Image imagen;
                string strRutaImagen;
                if (intTotalFotos >= 1)
                {
                    strRutaImagen = @"C:\PERSONAL\Pers_GIT\ENEL_IST_Inspections\ENEL\FOTOS_EQUIPOS\" + dsResultEnc.Tables[0].Rows[i]["Sede"].ToString() + @"\" + dsResultEnc.Tables[0].Rows[i]["Fotos"].ToString().Split('/')[0];
                    if (System.IO.File.Exists(strRutaImagen))
                    {
                        // Creamos la imagen y le ajustamos el tamaño
                        imagen = iTextSharp.text.Image.GetInstance(strRutaImagen);
                        imagen.BorderWidth = 0;
                        imagen.Alignment = Element.ALIGN_RIGHT;
                        double percentage = 0.0F;
                        percentage = 100 / (double)imagen.Width;
                        imagen.ScalePercent((float)percentage * 100);



                        celda1 = new iTextSharp.text.pdf.PdfPCell(imagen);
                        celda1.Colspan = 1;
                        celda1.Rowspan = 9;
                        celda1.Padding = 5;
                        celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;




                        table.AddCell(celda1);
                    }
                    else
                    {
                        celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                        celda1.Colspan = 1;
                        celda1.Rowspan = 9;
                        celda1.Padding = 5;
                        celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        table.AddCell(celda1);
                    }
                }
                else
                {
                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                    celda1.Colspan = 1;
                    celda1.Rowspan = 9;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    table.AddCell(celda1);
                }

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Equipo:" + dsResultEnc.Tables[0].Rows[i]["Equipo"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Referencia:" + dsResultEnc.Tables[0].Rows[i]["Referencia"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Marca:" + dsResultEnc.Tables[0].Rows[i]["Marca"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Serial:" + dsResultEnc.Tables[0].Rows[i]["Serial"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);





                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Lote:" + dsResultEnc.Tables[0].Rows[i]["Lote"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Ubicación:" + dsResultEnc.Tables[0].Rows[i]["Ubicacion"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Fecha de Fabricación:" + dsResultEnc.Tables[0].Rows[i]["FechaFabricacion"].ToString(), fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Responsable del equipo:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                document.Add(table);


                table = new PdfPTable(3);
                table.TotalWidth = 550.0F;
                table.LockedWidth = true;
                table.DefaultCell.BorderWidth = 20;
                Columns1 = new int[] { 400, 100, 100 };
                table.SetWidths(Columns1);
                table.SpacingBefore = 1.0F;
                table.SpacingAfter = 1.0F;
                table.DefaultCell.Border = Element.RECTANGLE;
                fuenteEncabezado = FontFactory.GetFont("Bookman Old Style", 9, iTextSharp.text.Font.BOLD);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Programa de Inspección", fuenteEncabezado));
                celda1.Colspan = 3;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Realizado Por:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Fecha de Inspección", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Resultado", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                int id;

                id = Convert.ToInt32(dsResultEnc.Tables[0].Rows[i]["id"]);
                DataSet dsResultados = new DataSet();
                dsResultEnc.Tables.Add(_WorkingAtHeightBo.GetResultbyElement(id));
    
                foreach (DataRow oRow in dsResultados.Tables[0].Rows)
                {
                //}

                //foreach (DataRow oRow in dsResultEnc.Tables[0].Rows)
                //{
                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(oRow["RealizadoPor"].ToString(), fuenteEncabezado));
                    celda1.Colspan = 1;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    table.AddCell(celda1);


                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(oRow["FechaInspeccion"].ToString(), fuenteEncabezado));
                    celda1.Colspan = 1;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    table.AddCell(celda1);

                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(oRow["Estado"].ToString(), fuenteEncabezado));
                    celda1.Colspan = 1;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    table.AddCell(celda1);

                }

                //// 2
                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);


                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);
                //// 3

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);


                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);
                //// 4

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);


                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);
                //// 5

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);


                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);
                //// 6

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);

                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);


                //celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                //celda1.Colspan = 1;
                //celda1.Padding = 5;
                //celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                //table.AddCell(celda1);



                document.Add(table);
                table = new PdfPTable(3);
                table.TotalWidth = 550.0F;
                table.LockedWidth = true;
                table.DefaultCell.BorderWidth = 20;
                Columns1 = new int[] { 400, 100, 100 };
                table.SetWidths(Columns1);
                table.SpacingBefore = 1.0F;
                table.SpacingAfter = 1.0F;
                table.DefaultCell.Border = Element.RECTANGLE;
                fuenteEncabezado = FontFactory.GetFont("Bookman Old Style", 9, iTextSharp.text.Font.BOLD);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Programa de mantenimiento", fuenteEncabezado));
                celda1.Colspan = 3;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Descripción del mantenimiento", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Fecha de Mantenimiento", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Realizado Por:", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                if (!dsResultEnc.Tables[0].Rows[i]["FechaRealizar"].ToString().Contains("1900"))
                {
                    // 1
                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["GestionRealizar"].ToString(), fuenteEncabezado));
                    celda1.Colspan = 1;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    table.AddCell(celda1);

                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["FechaRealizar"].ToString(), fuenteEncabezado));
                    celda1.Colspan = 1;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    table.AddCell(celda1);

                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["ResponsableRealizar"].ToString(), fuenteEncabezado));
                    celda1.Colspan = 1;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    table.AddCell(celda1);
                }
                else
                {
 // 2
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);
                }
                // 2
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);
                // 3
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                // 4
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);
                // 5
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);


                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 1;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                table.AddCell(celda1);

                document.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 550.0F;
                table.LockedWidth = true;
                table.DefaultCell.BorderWidth = 20;
                Columns1 = new int[] { 500 };
                table.SetWidths(Columns1);
                table.SpacingBefore = 1.0F;
                table.SpacingAfter = 1.0F;
                table.DefaultCell.Border = Element.RECTANGLE;
                fuenteEncabezado = FontFactory.GetFont("Bookman Old Style", 9, iTextSharp.text.Font.BOLD);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Observaciones", fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase(dsResultEnc.Tables[0].Rows[i]["Observaciones"].ToString(), fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);

                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);
                celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("", fuenteEncabezado));
                celda1.Colspan = 2;
                celda1.Padding = 5;
                celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                table.AddCell(celda1);
                document.Add(table);

                if (intTotalFotos > 1)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 550.0F;
                    table.LockedWidth = true;
                    table.DefaultCell.BorderWidth = 20;
                    Columns1 = new int[] { 600 };
                    table.SetWidths(Columns1);
                    table.SpacingBefore = 1.0F;
                    table.SpacingAfter = 1.0F;
                    table.DefaultCell.Border = Element.RECTANGLE;
                    fuenteEncabezado = FontFactory.GetFont("Bookman Old Style", 9, iTextSharp.text.Font.BOLD);

                    celda1 = new iTextSharp.text.pdf.PdfPCell(new Phrase("Otras Fotografias", fuenteEncabezado));
                    celda1.Colspan = 2;
                    celda1.Padding = 5;
                    celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                    table.AddCell(celda1);

                    int ni = 1;

                    while ((ni < intTotalFotos))
                    {
                        strRutaImagen = @"C:\PERSONAL\Pers_GIT\ENEL_IST_Inspections\ENEL\FOTOS_EQUIPOS\" + dsResultEnc.Tables[0].Rows[i]["Sede"].ToString() + @"\" + dsResultEnc.Tables[0].Rows[i]["Fotos"].ToString().Split('/')[ni];

                        if (System.IO.File.Exists(strRutaImagen))
                        {
                            // Creamos la imagen y le ajustamos el tamaño
                            imagen = iTextSharp.text.Image.GetInstance(strRutaImagen);
                            imagen.BorderWidth = 0;
                            imagen.Alignment = Element.ALIGN_RIGHT;
                            double percentage = 0.0F;
                            percentage = 100 / (double)imagen.Width;
                            imagen.ScalePercent((float)percentage * 100);

                            celda1 = new iTextSharp.text.pdf.PdfPCell(imagen);
                            celda1.Colspan = 1;
                            celda1.Rowspan = 1;
                            celda1.Padding = 5;
                            celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            table.AddCell(celda1);
                        }
                        ni = (ni + 1);
                    }
                    document.Add(table);

                }
                document.Close();
                //fin
            }
            catch (Exception ex)
            {

            }
        }
    }
}
