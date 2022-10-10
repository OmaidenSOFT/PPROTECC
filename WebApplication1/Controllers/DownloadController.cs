using LogicBo;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [Access]
    public class DownloadController : Controller
    {
        #region Properties
        WorkingAtHeightBo _workingAtHeightBo = new WorkingAtHeightBo();
        TrainningBo _trainningBo = new TrainningBo();
        IzageBo _izageBo = new IzageBo();
        PersonBo _personBo = new PersonBo();
        InspectionIzajeBo _inspectionIzajeBo = new InspectionIzajeBo();
        InspectionsBo _inspectionsBo = new InspectionsBo();
        #endregion
        public ActionResult DownloadFile(string fileName, string id, string id1, string id2)
        {
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(fileName);

                switch (fileName)
                {
                    case "Stock":
                        int countryID = Convert.ToInt32(base.Session["CountryID"]);
                        ws.Cells["A1"].LoadFromDataTable(_workingAtHeightBo.GetStock(countryID), true);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "StockPorSede":
                        ws.Cells["A1"].LoadFromDataTable(_workingAtHeightBo.GetStockByHeadquarter(int.Parse(id)), true);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "CronogramaFormacion":
                        int headquarterId = Convert.ToInt32(id);
                        int schedulerType = Convert.ToInt32(id1.ToString());
                        int year = Convert.ToInt32(id2.ToString());
                        int order = 1;
                        ws.Cells["A1"].LoadFromDataTable(_trainningBo.GetSearchScheduler(headquarterId, schedulerType, order), true);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "DetalleCronograma":
                        int headQuarterId = Convert.ToInt32(id);
                        int year1 = Convert.ToInt32(id2.ToString());
                        int schedulerType1 = Convert.ToInt32(id1.ToString());
                        ws.Cells["A1"].LoadFromDataTable(_trainningBo.GetLoadDetailsScheduler(headQuarterId, schedulerType1, id2.ToString(), 1, 1), true);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "DetalleUsuario":
                        int userId = Convert.ToInt32(id);
                        ws.Cells["A1"].LoadFromDataTable(_trainningBo.GetLoadDetailsUser(userId), true);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "ListadoRolAptitud":
                        int cbxHeadquarter = Convert.ToInt32(id);

                        ws.Cells["A1"].LoadFromDataTable(_trainningBo.GetSearchTrainningByHeadquarter(cbxHeadquarter), true);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "Hallazgos":
                        int headQuarterTypeid = Convert.ToInt32(id);
                        ws.Cells["A1"].LoadFromDataTable(_inspectionsBo.SearchManagementOfFindings(headQuarterTypeid), true);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "Izaje":
                        List<int> ColumnasFechas = new List<int>() { 6 };
                        int idIzaje = Convert.ToInt32(id);
                        ws.Cells["A1"].LoadFromDataTable(_izageBo.GetIndex("", 0, "", "", "", ""), true);
                        foreach (int col in ColumnasFechas)
                            ws.Column(col).Style.Numberformat.Format = "dd/MM/yyyy";
                        ws.Cells["A1:AY1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["A1:AY1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#002060"));
                        ws.Cells["A1:AY1"].Style.Font.Bold = true;
                        ws.Cells["A1:AY1"].Style.Font.Color.SetColor(Color.White);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "Personas":
                        ws.Cells["A1"].LoadFromDataTable(_personBo.GetIndex("", "", 0, "", ""), true);
                        
                        ws.Cells["A1:AY1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["A1:AY1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#002060"));
                        ws.Cells["A1:AY1"].Style.Font.Bold = true;
                        ws.Cells["A1:AY1"].Style.Font.Color.SetColor(Color.White);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;

                    case "InspeccionIzaje":
                        List<int> ColumnasFechas1 = new List<int>() { };
                        ws.Cells["A1"].LoadFromDataTable(_inspectionIzajeBo.GetInfo(), true);
                        foreach (int col in ColumnasFechas1)
                            ws.Column(col).Style.Numberformat.Format = "dd/MM/yyyy";
                        ws.Cells["A1:AY1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells["A1:AY1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#002060"));
                        ws.Cells["A1:AY1"].Style.Font.Bold = true;
                        ws.Cells["A1:AY1"].Style.Font.Color.SetColor(Color.White);
                        ws.Cells.AutoFitColumns();
                        stream = new MemoryStream(pack.GetAsByteArray()); //Get updated stream
                        break;
                }

            }
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
        }
    }
}
