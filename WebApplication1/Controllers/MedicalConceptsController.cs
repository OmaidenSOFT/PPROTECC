using LogicBo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Access]
    public class MedicalConceptsController : Controller
    {

        #region Properties
        HeadquarterBo _headquarterBo = new HeadquarterBo();
        TrainningBo _trainningBo = new TrainningBo();
        MedicalConceptsBo _medicalConceptsBo = new MedicalConceptsBo();
        #endregion
        public ActionResult CreateAptitudConcept()
        {
            Session["FilesMedicalConcepts"] = null;
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.ConceptsDictionary = new SelectList(_medicalConceptsBo.GetConceptsDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult PageInitial()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult SearchPageInitial(FormCollection collection)
        {
            try
            {
                int headQuarterType = collection["headQuarterType"].ToString() != string.Empty ? Convert.ToInt32(collection["headQuarterType"].ToString()) : 0;
                var result = _medicalConceptsBo.SearchMedicalConcepts(headQuarterType);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region JsonResult
        [HttpPost]
        public JsonResult GetEmployeeByHeadquarter(int idHeadquarter)
        {
            try
            {
                var result = _medicalConceptsBo.GetEmployeeByHeadquarter(idHeadquarter);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
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
                    var utlityModels = Session["FilesMedicalConcepts"] as List<UtilityModels>;
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
                            Session["FilesMedicalConcepts"] = objectSession;
                        }
                        else
                        {
                            utlityModels.Add(new UtilityModels
                            {
                                file = binaryData,
                                name = name,
                                extension = ext,
                                type = collection["type"]
                            });
                            Session["FilesMedicalConcepts"] = utlityModels;


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

        public JsonResult ProcessCreateAptitudConcept(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                //int headquarterId = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int employeeId = collection["cbxEmployee"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxEmployee"].ToString()) : 0;
                int conceptId = collection["cbxConcept"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxConcept"].ToString()) : 0;
                DateTime conceptDate;
                try
                {
                    conceptDate = Convert.ToDateTime(collection["cconceptDate"]);
                }
                catch (Exception ex)
                {
                    conceptDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                }
                string folderName = "MEDICALCONCEPTS";
                string fileName = string.Empty;

                foreach (var item in Session["FilesMedicalConcepts"] as List<UtilityModels>)
                {
                    string path;
                    switch (item.type)
                    {
                        case "Concept":
                            path = string.Format("~/{0}", folderName);
                            Directory.CreateDirectory(Server.MapPath(path));
                            using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + employeeId + '_' + item.name + item.extension)))
                            {
                                file.Write(item.file, 0, item.file.Length);
                            }
                            fileName = item.name + item.extension;
                            break;
                        default:
                            break;
                    }

                }
                Session["FilesMedicalConcepts"] = null;
                var result = _medicalConceptsBo.CreateAptitudConcept(employeeId, conceptId, conceptDate, fileName);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        #endregion
    }
}