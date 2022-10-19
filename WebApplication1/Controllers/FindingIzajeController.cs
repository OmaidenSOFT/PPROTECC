using LogicBo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Utils;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FindingIzajeController : Controller
    {
        // GET: FindingIzaje
        #region Properties
        WorkingAtHeightBo _workingAtHeightBo = new WorkingAtHeightBo();
        HeadquarterBo _headquarterBo = new HeadquarterBo();
        CategoryBo _categoryBo = new CategoryBo();
        ElementBo _elementBo = new ElementBo();
        LocationBo _locationBo = new LocationBo();
        MarkBo _markBo = new MarkBo();
        CurriculumBo _curriculumBo = new CurriculumBo();
        TechnicalInformationBo _technicalInformationBo = new TechnicalInformationBo();
        EquipmentBo _equipmentBo = new EquipmentBo();
        IzageBo _izageBo = new IzageBo();
        InspectionIzajeBo _inspectionIzajeBo = new InspectionIzajeBo();
        TrainningBo _trainningBO = new TrainningBo();
        ActionResultBo _actionResultBo = new ActionResultBo();
        FinalStateBo _finalStateBo = new FinalStateBo();
        Util util;
        FactoresIzajeBo _factoresIzajeBo = new FactoresIzajeBo();

        #endregion
        public ActionResult Index()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryWithoutAll(), "Key", "Value");
            var result = _inspectionIzajeBo.GetInfoFinding();
            return PartialView(result);
        }

        public PartialViewResult SearchFindingIzage()
        {
            try
            {
                var result = _inspectionIzajeBo.GetInfoFinding();
                return PartialView(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //[HttpPost]
        public PartialViewResult LoadDetailsEquipment(int id)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");
                Session["FilesInspections"] = null;
                var result = _inspectionIzajeBo.GetFindingById(id);
                //int stateId = int.Parse(result.Rows[0]["Id"].ToString());
                ViewBag.ActionResultDictionary = new SelectList(_actionResultBo.GetDictionary(), "Key", "Value", result.Rows[0]["stateid"].ToString());
                ViewBag.finding = result;
                ViewBag.id = id;
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult SaveDetailsEquipmentWMan(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int id = collection["id"].ToString() != string.Empty ? Convert.ToInt32(collection["id"].ToString()) : 0;
                int actionTomar = collection["cbxActionResult"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxActionResult"].ToString()) : 0;
                string gestionRealizar = collection["gestionRealizar"].ToString();
                string responsable = collection["responsable"].ToString();
                DateTime managmentDate = Convert.ToDateTime(collection["cManagmentDate"]);


                _inspectionIzajeBo.EditFinding(id, actionTomar, "Abierto", gestionRealizar, responsable, managmentDate, 0, DateTime.Now, "", "", 2);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult SaveDetailsEquipmentOpen(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int id = collection["id"].ToString() != string.Empty ? Convert.ToInt32(collection["id"].ToString()) : 0;
                int actionTomar = collection["cbxActionResult"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxActionResult"].ToString()) : 0;
                string gestionRealizar = collection["gestionRealizar"].ToString();
                string gestionRealizada = collection["gestionRealizada"].ToString();
                string responsable = collection["responsable"].ToString();

                DateTime managmentDate = Convert.ToDateTime(collection["cManagmentDate"]);
                DateTime managmentDate2 = Convert.ToDateTime(collection["cManagmentDate2"]);

                string folderName = "Archivo_Hallazgo";
                string fileName = string.Empty;

                foreach (var item in Session["FilesInspections"] as List<UtilityModels>)
                {
                    string path;
                    switch (item.type)
                    {
                        case "file":
                            path = string.Format("~/{0}", folderName);
                            Directory.CreateDirectory(Server.MapPath(path));
                            using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + item.name + item.extension)))
                            {
                                file.Write(item.file, 0, item.file.Length);
                            }
                            fileName = item.name + item.extension;
                            break;
                        default:
                            break;
                    }

                }
                Session["FilesInspections"] = null;


                _inspectionIzajeBo.EditFinding(id, actionTomar, "Abierto", gestionRealizar, responsable, managmentDate2, 0, managmentDate, gestionRealizada, fileName, 3);


                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UploadFile(FormCollection collection)
        {
            try
            {
                var path = String.Empty;

                foreach (string filetmps in Request.Files)
                {
                    var utlityModels = Session["FilesInspections"] as List<UtilityModels>;
                    HttpPostedFileBase filetmp = Request.Files[filetmps];
                    if (filetmp != null)
                    {
                        var b = new BinaryReader(filetmp.InputStream);
                        int length = Convert.ToInt32(filetmp.InputStream.Length);
                        byte[] binaryData = b.ReadBytes(length);
                        var ext = Path.GetExtension(filetmp.FileName);
                        var name = Path.GetFileNameWithoutExtension(filetmp.FileName);
                        if (utlityModels == null)
                        {
                            var objectSession = new List<UtilityModels>
                            {
                                new UtilityModels{
                                file= binaryData,
                                name=name,
                                extension = ext,
                                type=collection["type"]

                                }

                            };
                            Session["FilesInspections"] = objectSession;
                        }
                        else
                        {
                            utlityModels.Add(new UtilityModels
                            {
                                extension = ext,
                                name = name,
                                file = binaryData,
                                type = collection["type"]
                            });
                            Session["FilesInspections"] = utlityModels;

                        }
                    }
                }

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }

        }
    }
}