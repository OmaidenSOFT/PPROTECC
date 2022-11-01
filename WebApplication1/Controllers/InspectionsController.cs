using LogicBo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using Utils;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Access]
    public class InspectionsController : Controller
    {
        #region Properties
        WorkingAtHeightBo _workingAtHeightBo = new WorkingAtHeightBo();
        HeadquarterBo _headquarterBo = new HeadquarterBo();
        CategoryBo _categoryBo = new CategoryBo();
        ElementBo _elementBo = new ElementBo();
        LocationBo _locationBo = new LocationBo();
        TrainningBo _trainningBO = new TrainningBo();
        ActionResultBo _actionResultBo = new ActionResultBo();
        MarkBo _markBo = new MarkBo();
        CurriculumBo _curriculumBo = new CurriculumBo();
        TechnicalInformationBo _technicalInformationBo = new TechnicalInformationBo();
        EquipmentBo _equipmentBo = new EquipmentBo();
        InspectionsBo _inspectionsBo = new InspectionsBo();
        FinalStateBo _finalStateBo = new FinalStateBo();
        Util util;
        #endregion


        public ActionResult Stock()
        {
            var model = _inspectionsBo.GetStock();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult StockByHeadquarter(int headquarterId)
        {
            ViewBag.id = headquarterId;
            var model = _inspectionsBo.GetStockByHeadquarter(headquarterId);
            return PartialView(model);
        }
        public ActionResult Curriculum()
        {
            //var model = _workingAtHeightBo.GetStock();
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.CategoryDictionary = new SelectList(_categoryBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult TechnicalInformation()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.CategoryDictionary = new SelectList(_categoryBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult IndexEquipment()
        {
            var result = _equipmentBo.GetIndex(Convert.ToInt32(Session["CountryID"]));
            return PartialView(result);
        }
        public ActionResult CreateInspections(int? headquarterId, string RFID, int? elementid)
        {
            ViewBag.headquarterId = headquarterId;
            ViewBag.elementid = elementid;
            ViewBag.RFID = RFID;
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            //ViewBag.CategoryDictionary = new SelectList(_categoryBo.GetDictionary(), "Key", "Value");
            ViewBag.ElementDictionary = new SelectList(_elementBo.GetDictionary(), "Key", "Value");
            ViewBag.LocationDictionary = new SelectList(_locationBo.GetDictionary(), "Key", "Value");
            ViewBag.TsaDictionary = new SelectList(_trainningBO.GetDictionary(), "Key", "Value");
            ViewBag.InspectorDictionary = new SelectList(_trainningBO.GetInspectorDictionary(), "Key", "Value");
            ViewBag.ActionResultDictionary = new SelectList(_actionResultBo.GetDictionary(), "Key", "Value");
            ViewBag.FinalStateDictionary = new SelectList(_finalStateBo.GetDictionary(), "Key", "Value");
            ViewBag.StateDictionary = new SelectList(_finalStateBo.GetStatesDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult SchedulerInspections()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryWithoutAll(), "Key", "Value");
            ViewBag.ElementDictionary = new SelectList(_elementBo.GetDictionary(), "Key", "Value");
            ViewBag.TypeDictionary = new SelectList(_elementBo.GetDictionaryByType(), "Key", "Value");
            return PartialView();
        }
        public ActionResult CertificatesReportsIndex()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryheadQuarterType(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        public ActionResult FormatsReportsIndex()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryheadQuarterType(Convert.ToInt32(Session["CountryID"])), "Key", "Value");

            return PartialView();
        }
        public ActionResult ProceduresReportsIndex()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryheadQuarterType(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        public ActionResult LegalityReportsIndex()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryheadQuarterType(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        public ActionResult ManagementOfFindings()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        public ActionResult ManagementMaintenanceEquipment()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        public ActionResult CheckListArnes()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.LocationDictionary = new SelectList(_locationBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }

        #region JsonResult
        [HttpPost]
        public JsonResult GetElementByCategory(int idCategory)
        {
            try
            {
                var result = _elementBo.GetDictionaryByCategory(idCategory);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public JsonResult GetDataElementByRFID(string rfid, int headquaterid)
        {
            try
            {
                var result = _elementBo.GetDataElementByRFID(rfid, headquaterid);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }

        }
        [HttpPost]
        public JsonResult GetFactorByElement(string rfid, int headquarterid)
        {
            try
            {
                var result = _inspectionsBo.GetFactorByElement(rfid, headquarterid);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetMarkByHeadquarterElement(int idHeadquarter, int idElement)
        {
            try
            {
                var result = _markBo.GetDictionaryByHeadquarterElement(idHeadquarter, idElement);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpPost]
        public JsonResult ProcessCreateEquipment(FormCollection collection, HttpPostedFileBase file)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string rFID = collection["rFID"];
                string serial = collection["serial"];
                string model = collection["model"];
                string material = collection["material"];
                DateTime fabricationDate = Convert.ToDateTime(collection["cfabricationDate"]);
                string mark = collection["mark"];
                string lot = collection["lot"];
                int element = collection["cbxElement"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxElement"].ToString()) : 0; ;
                int headquarter = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                DateTime purchaseDate = Convert.ToDateTime(collection["cpurchaseDate"]);
                int cbxAssigned = collection["cbxAssigned"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAssigned"].ToString()) : 0;
                int? areaId = null;
                int? employedId = null;
                int ubicationID = collection["cbxLocation"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxLocation"].ToString()) : 0;

                if (cbxAssigned == 1)
                {
                    areaId = Convert.ToInt32(collection["cbxFieldAssigned"].ToString());
                }
                else if (cbxAssigned == 2)
                {
                    employedId = Convert.ToInt32(collection["cbxFieldAssigned"].ToString());
                }
                int cbxAssignedDate = collection["cbxAssignedDate"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAssignedDate"].ToString()) : 0;

                var idEquipment = _curriculumBo.Create(rFID, serial, model, material, fabricationDate, mark, lot, element, headquarter, purchaseDate, areaId, employedId, cbxAssignedDate, ubicationID);

                util = new Util();
                util.CreateCurriculum(idEquipment);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ProcessCreateInspections(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int id = collection["id"].ToString() != string.Empty ? Convert.ToInt32(collection["id"].ToString()) : 0;
                int cbxLocation = collection["cbxLocation"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxLocation"].ToString()) : 0;
                int cbxFinalState = collection["cbxFinalState"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxFinalState"].ToString()) : 0;
                int cbxActionResult = collection["cbxActionResult"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxActionResult"].ToString()) : 0;
                int cbxInspector = collection["cbxInspector"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxInspector"].ToString()) : 0;
                DateTime inspectionDate = Convert.ToDateTime(collection["cinspectionDate"]);

                bool result = _inspectionsBo.UpdateInspection(id, cbxFinalState, cbxActionResult, inspectionDate, cbxInspector, cbxLocation);

                if (result)
                {
                    var elements = collection.AllKeys.Where(x => x.StartsWith("comment_"));
                    foreach (var item in elements)
                    {
                        var element = item.Split('_');
                        var idFactor = element[1];
                        var comment = collection["comment_" + idFactor];
                        var state = int.Parse(collection["factor_" + idFactor]);

                        _inspectionsBo.UpdateFactorsByInspection(state, comment, int.Parse(idFactor), id);
                    }
                }

                util = new Util();
                util.CreateCurriculum(id);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
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




        [HttpPost, ValidateInput(false)]
        public JsonResult CreateTechInfo(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int headQuarterType = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int Elementoid = collection["cbxElement"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxElement"].ToString()) : 0;
                string Marca = collection["mark"];
                string Modelo = collection["model"];
                string Observaciones = collection["obser"];
                


                string folderName = string.Empty;
                string fileName = string.Empty;
                folderName = "FICHAS_TEC";

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

                var result = _inspectionsBo.TechInfoCreate(headQuarterType, Elementoid,Marca,Modelo,Observaciones,fileName);


                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost, ValidateInput(false)]
        public JsonResult FeaturesCreate(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int cbxFeatures = collection["cbxFeatures"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxFeatures"].ToString()) : 0;
                int headQuarterType = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                string name = collection["name"];

                string folderName = string.Empty;
                string fileName = string.Empty;
                switch (cbxFeatures)
                {
                    case 1:
                        folderName = "PROCEDURE_reports";
                        break;
                    case 2:
                        folderName = "FORMATS_reports";
                        break;
                    case 3:
                        folderName = "CERTIFICATES_reports";
                        break;
                    case 4:
                        folderName = "LEGALITY_reports";
                        break;
                }


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

                var result = _inspectionsBo.FeaturesCreate(headQuarterType, cbxFeatures, name, fileName);


                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost, ValidateInput(false)]
        //public JsonResult CreateTechInfo(FormCollection collection)
        //{
        //    try
        //    {
        //        var session = Session["SessionUser"] as SessionModels;
        //        if (session == null)
        //            throw new Exception("Se ha perdido la sesión del Usuario");

        //        int cbxFeatures = collection["cbxFeatures"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxFeatures"].ToString()) : 0;
        //        int headQuarterType = collection["headQuarterType"].ToString() != string.Empty ? Convert.ToInt32(collection["headQuarterType"].ToString()) : 0;
        //        string name = collection["name"];

        //        string folderName = string.Empty;
        //        string fileName = string.Empty;
        //        switch (cbxFeatures)
        //        {
        //            case 1:
        //                folderName = "PROCEDURE_reports";
        //                break;
        //            case 2:
        //                folderName = "FORMATS_reports";
        //                break;
        //            case 3:
        //                folderName = "CERTIFICATES_reports";
        //                break;
        //            case 4:
        //                folderName = "LEGALITY_reports";
        //                break;
        //        }


        //        foreach (var item in Session["FilesInspections"] as List<UtilityModels>)
        //        {
        //            string path;
        //            switch (item.type)
        //            {
        //                case "file":
        //                    path = string.Format("~/{0}", folderName);
        //                    Directory.CreateDirectory(Server.MapPath(path));
        //                    using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + item.name + item.extension)))
        //                    {
        //                        file.Write(item.file, 0, item.file.Length);
        //                    }
        //                    fileName = item.name + item.extension;
        //                    break;
        //                default:
        //                    break;
        //            }

        //        }
        //        Session["FilesInspections"] = null;

        //        var result = _inspectionsBo.FeaturesCreate(headQuarterType, cbxFeatures, name, fileName);


        //        return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
        //    }
        //}

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
                string gestion = collection["gestion"].ToString();
                string responsable = collection["responsable"].ToString();
                DateTime managmentDate = Convert.ToDateTime(collection["cManagmentDate"]);


                _inspectionsBo.ManagmentOfFindingsSave(id, actionTomar, gestion, responsable, managmentDate, null, null, null, 2);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveDetailsEquipmentOpen(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int id = collection["id"].ToString() != string.Empty ? Convert.ToInt32(collection["id"].ToString()) : 0;
                string gestion = collection["gestion"].ToString();
                DateTime managmentDate = Convert.ToDateTime(collection["cManagmentDate"]);

                string folderName = "MANAGMENTS_FILES";
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


                _inspectionsBo.ManagmentOfFindingsSave(id, null, null, null, null, managmentDate, gestion, fileName, 3);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateVerificateAction(int id)
        {
            try
            {
                _inspectionsBo.UpdateVerificateActionPlan(id);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetActionResultByState(int idState)
        {
            try
            {
                var result = _actionResultBo.GetDictionaryByState(idState);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        #endregion

        #region PartialView
        [HttpPost]
        public PartialViewResult SearchCurriculum(FormCollection collection)
        {
            try
            {
                string rFID = string.IsNullOrEmpty(collection["rFID"].ToString()) ? null : collection["rFID"].ToString();
                int tempcbxHeadquarter;
                int tempcbxElement;
                int? cbxHeadquarter = int.TryParse(collection["cbxHeadquarter"].ToString(), out tempcbxHeadquarter) ? tempcbxHeadquarter : (int?)null;
                int? cbxElement = Int32.TryParse(collection["cbxElement"].ToString(), out tempcbxElement) ? tempcbxElement : (int?)null;

                var result = _curriculumBo.GetListByElement(cbxHeadquarter, cbxElement, rFID);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult SearchManagementMaintenanceEquipment(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.ManagementMaintenanceEquipment(headQuarterTypeid);
                ViewBag.id = collection["cbxHeadquarter"].ToString();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult SearchManagementOfFindings(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.SearchManagementOfFindings(headQuarterTypeid);
                ViewBag.id = collection["cbxHeadquarter"].ToString();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public PartialViewResult SearchCertificatesT(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.GetCertificatesReportsIndex(headQuarterTypeid);
                ViewBag.id = collection["cbxHeadquarter"].ToString();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        [HttpPost]
        public PartialViewResult SearchFormatsT(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.GetFormatsReportsIndex(headQuarterTypeid);
                ViewBag.id = collection["cbxHeadquarter"].ToString();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        [HttpPost]
        public PartialViewResult SearchProceduresT(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.GetProceduresReportsIndex(headQuarterTypeid);
                ViewBag.id = collection["cbxHeadquarter"].ToString();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        [HttpPost]
        public PartialViewResult SearchLegalityT(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.GetLegalityReportsIndex(headQuarterTypeid);
                ViewBag.id = collection["cbxHeadquarter"].ToString();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        [HttpPost]
        public PartialViewResult SearchSchedulerInspections(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int elementid = collection["cbxElement"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxElement"].ToString()) : 0;
                int typeid = collection["cbxScheduleType"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxScheduleType"].ToString()) : 0;
                var result = _inspectionsBo.SearchSchedulerInspections(headQuarterTypeid, typeid, elementid);


                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult UserResult(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.SearchManagementOfFindings(headQuarterTypeid);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        


        [HttpPost]
        public PartialViewResult TechnicalReportsResult(FormCollection collection)
        {
            try
            {
                int headQuarterTypeid = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                var result = _inspectionsBo.SearchManagementOfFindings(headQuarterTypeid);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartialViewResult CreateTechnicalReports()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.TypeReportDictionary = new SelectList(_inspectionsBo.GetTypeReportDictionary(), "Key", "Value");
            return PartialView();
        }
        public PartialViewResult Features()
        {
            Session["FilesInspections"] = null;
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryheadQuarterType(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.FeaturesDictionary = new SelectList(_inspectionsBo.GetFeaturesDictionary(), "Key", "Value");
            return PartialView();
        }
        public PartialViewResult CreateTechInfo()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.CategoryDictionary = new SelectList(_categoryBo.GetDictionary(), "Key", "Value");
            Session["FilesInspections"] = null;
            ViewBag.FeaturesDictionary = new SelectList(_inspectionsBo.GetFeaturesDictionary(), "Key", "Value");
            return PartialView();
        }
        public PartialViewResult TechnicalReportsIndex()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult SearchTechnicalReports(FormCollection collection)
        {
            int idHeadquarter = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
            var result = _inspectionsBo.GetTechnicalReportsIndex(idHeadquarter);
            return PartialView(result);
        }
        public PartialViewResult ProceduresReports(FormCollection collection)
        {
            int idHeadquarter = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
            var result = _inspectionsBo.GetProceduresReportsIndex(idHeadquarter);
            return PartialView(result);
        }
        public PartialViewResult FormatsReports(FormCollection collection)
        {
            int idHeadquarter = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
            var result = _inspectionsBo.GetFormatsReportsIndex(idHeadquarter);
            return PartialView(result);
        }
        public PartialViewResult CertificatesReports(FormCollection collection)

        {
            int idHeadquarter = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
            var result = _inspectionsBo.GetFormatsReportsIndex(idHeadquarter);
            return PartialView(result);
        }
        public PartialViewResult LegalityReports(FormCollection collection)

        {
            int idHeadquarter = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
            var result = _inspectionsBo.GetLegalityReportsIndex(idHeadquarter);
            return PartialView(result);
        }

        [HttpPost]
        public PartialViewResult LoadDetailsEquipment(int id)
        {
            try
            {
                Session["FilesInspections"] = null;
                var result = _inspectionsBo.LoadDetailsEquipment(id);
                int stateId = int.Parse(result.Rows[0]["stateid"].ToString());
                ViewBag.ActionResultDictionary = new SelectList(_actionResultBo.GetDictionaryByState(stateId), "Key", "Value");
                ViewBag.finding = _inspectionsBo.SearchFindingsByElement(id);
                ViewBag.id = id;
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult SearchTechnicalInformation(FormCollection collection)
        {
            try
            {
                int cbxMark = Int32.Parse(collection["cbxMark"].ToString());

                var result = _technicalInformationBo.GetByMark(cbxMark);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


    }
}
