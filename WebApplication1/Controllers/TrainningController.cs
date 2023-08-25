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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Access]
    public class TrainningController : Controller
    {
        #region Properties
        WorkingAtHeightBo _workingAtHeightBo = new WorkingAtHeightBo();
        HeadquarterBo _headquarterBo = new HeadquarterBo();
        CategoryBo _categoryBo = new CategoryBo();
        ElementBo _elementBo = new ElementBo();
        MarkBo _markBo = new MarkBo();
        CurriculumBo _curriculumBo = new CurriculumBo();
        TechnicalInformationBo _technicalInformationBo = new TechnicalInformationBo();
        EquipmentBo _equipmentBo = new EquipmentBo();
        TrainningBo _trainningBo = new TrainningBo();
        string _pathPerson = ConfigurationManager.AppSettings["PathPerson"].ToString();
        string _pathInspector = ConfigurationManager.AppSettings["PathInspector"].ToString();
        private readonly Transversal.EncryptData encryptData = new Transversal.EncryptData();
        #endregion
        public ActionResult Trainning()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        public ActionResult Certificate()
        {
            return PartialView();
        }
        public ActionResult IndexPerson()
        {
            var result = _trainningBo.GetIndexPerson();
            return PartialView(result);
        }

        public ActionResult IndexUser()
        {
            var result = _trainningBo.GetIndexUser();
            return PartialView(result);
        }
        public ActionResult IndexInspector()
        {
            var result = _trainningBo.GetIndexInspector();
            return PartialView(result);
        }

        public ActionResult IndexRol()
        {
            var result = _trainningBo.GetIndexRol();
            return PartialView(result);
        }

        public ActionResult Scheduler()
        {

            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.GetYearsDictionary = new SelectList(_headquarterBo.GetYearsDictionary(), "Key", "Value");

            return PartialView();
        }
        public ActionResult CreatePerson()
        {
            Session["FilesUser"] = null;
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            ViewBag.CourseInitialDictionary = new SelectList(_trainningBo.GetCourseInitialDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult CreateInspector()
        {
            Session["FilesUser"] = null;
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value");
            return PartialView();
        }
        public ActionResult CreateUser()
        {
            ViewBag.RoleDictionary = new SelectList(_headquarterBo.GetRoleDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult CreateRol()
        {
            //ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            //ViewBag.CourseInitialDictionary = new SelectList(_trainningBo.GetCourseInitialDictionary(), "Key", "Value");
            return PartialView();
        }

        public ActionResult EditPerson(int idPerson)
        {
            var model = _trainningBo.GetInfoPerson(idPerson);
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(Convert.ToInt32(Session["CountryID"])), "Key", "Value", model.Rows[0]["Sedeid"].ToString());
            return PartialView(model);
        }

        public ActionResult EditUser(int idUser)
        {
            var model = _trainningBo.GetInfoUser(idUser);
            ViewBag.RoleDictionary = new SelectList(_headquarterBo.GetRoleDictionary(), "Key", "Value", model.Rows[0]["RolId"].ToString());
            return PartialView(model);
        }
        public ActionResult EditInspector(int idInspector)
        {
             var model = _trainningBo.GetInfoInspector(idInspector);
            return PartialView(model);
        }

        [HttpPost]
        public PartialViewResult SearchCertificate(FormCollection collection)
        {
            try
            {
                string name = string.IsNullOrEmpty(collection["name"].ToString()) ? null : collection["name"].ToString();
                string identification = string.IsNullOrEmpty(collection["identification"].ToString()) ? null : collection["identification"].ToString();

                var result = _trainningBo.GetSearchCertificate(name, identification);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult SearchScheduler(FormCollection collection)
        {
            try
            {

                int tempcbxHeadquarter;
                int? headQuarterType = int.TryParse(collection["cbxHeadquarter"].ToString(), out tempcbxHeadquarter) ? tempcbxHeadquarter : (int?)0;
                int tempfrequency;
                int? schedulerType = int.TryParse(collection["frequency"].ToString(), out tempfrequency) ? tempfrequency : (int?)0;
                //  int year = collection["cbxYear"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxYear"].ToString()) : 0;
                var result = _trainningBo.GetSearchScheduler((int)headQuarterType, (int)schedulerType, 1);
                ViewBag.id = int.TryParse(collection["cbxHeadquarter"].ToString(), out tempcbxHeadquarter) ? tempcbxHeadquarter : (int?)0;
                ViewBag.id1 = int.TryParse(collection["frequency"].ToString(), out tempfrequency) ? tempfrequency : (int?)0;
                //ViewBag.id2 = collection["cbxYear"].ToString();
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult SearchTrainning(FormCollection collection)
        {
            try
            {
                int tempcbxHeadquarter;
                int? cbxHeadquarter = int.TryParse(collection["cbxHeadquarter"].ToString(), out tempcbxHeadquarter) ? tempcbxHeadquarter : (int?)null;

                var result = _trainningBo.GetSearchTrainningByHeadquarter(cbxHeadquarter);
                ViewBag.id = cbxHeadquarter;
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult LoadDetailsScheduler(int headQuarterId, string month, int type, int year)
        {
            try
            {
                var result = _trainningBo.GetLoadDetailsScheduler(headQuarterId, 1, string.IsNullOrEmpty(month) ? null : month, type, year);
                ViewBag.headQuarterId = headQuarterId;
                ViewBag.month = month;
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult LoadDetailsUser(int userId)
        {
            try
            {
                var result = _trainningBo.GetLoadDetailsUser(userId);
                ViewBag.userId = userId;
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartialViewResult LoadCourses(int idPerson, string identification, bool isView = false)
        {
            try
            {
                ViewBag.idPerson = idPerson;
                ViewBag.identification = identification;
                ViewBag.isView = isView;
                ViewBag.CourseDictionary = new SelectList(_trainningBo.GetCourseDictionary(), "Key", "Value");
                return PartialView();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public PartialViewResult LoadCoursesByPerson(int idPerson)
        {
            try
            {
                var model = _trainningBo.GetCoursesByPerson(idPerson);
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region JsonResult
        [HttpPost, ValidateInput(false)]
        public JsonResult ProcessCreatePerson(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string name = collection["name"];
                string identification = collection["identification"];
                string assigned = collection["assigned"];
                int headquarterId = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int courseInitialId = collection["cbxCourseInitial"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCourseInitial"].ToString()) : 0;
                string lastname = collection["lastname"];
                DateTime initialDate;
                try
                {
                    initialDate = Convert.ToDateTime(collection["cinitialDate"]);
                }
                catch (Exception ex)
                {
                    initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                }
                var idPerson = _trainningBo.CreatePerson(name, identification, assigned, headquarterId, initialDate, courseInitialId, lastname);
                if (idPerson > 0)
                {
                    var sessionFiles = Session["FilesUser"] as List<UtilityModels>;
                    if (sessionFiles != null)
                    {
                        foreach (var item in Session["FilesUser"] as List<UtilityModels>)
                        {
                            string path;
                            switch (item.type)
                            {
                                case "Photo":
                                    path = string.Format("~/{0}/Images/{1}", _pathPerson, idPerson);
                                    Directory.CreateDirectory(Server.MapPath(path));
                                    using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + identification + item.extension)))
                                    {
                                        file.Write(item.file, 0, item.file.Length);
                                    }
                                    break;
                                case "CourseInitial":
                                    path = string.Format("~/{0}/Courses/{1}/Initial", _pathPerson, idPerson);
                                    Directory.CreateDirectory(Server.MapPath(path));
                                    using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + identification + item.extension)))
                                    {
                                        file.Write(item.file, 0, item.file.Length);
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    Session["FilesUser"] = null;
                }
                return Json(new { result = true, idPerson, identification }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpPost]
        public JsonResult ProcessCreateUser(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string nombres = collection["name"];
                string apellidos = collection["lastname"];
                string email = collection["email"];
                string login = collection["login"];
                string password = collection["pass"];
                int headQuarterType = collection["headQuarterType"].ToString() != string.Empty ? Convert.ToInt32(collection["headQuarterType"].ToString()) : 0;
                var passwordEncrypt = encryptData.Encrypt(password, true);
                int RoleId = collection["cbxRole"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxRole"].ToString()) : 0;

                var idUser = _trainningBo.CreateUser(login, nombres, apellidos, email, passwordEncrypt, RoleId, headQuarterType);

                return Json(new { result = true, idUser, login }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpPost]
        public JsonResult ProcessCreateInspector(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");
                string apellidos = collection["lastname"];
                string nombres = collection["name"];
                string login = collection["login"];
                string password = collection["pass"];
                string identificacion = collection["pass"];
                var passwordEncrypt = encryptData.Encrypt(password, true);
                int enabledType = collection["enabledInspector"].ToString() != string.Empty ? Convert.ToInt32(collection["enabledInspector"].ToString()) : 0;

                var idUser = _trainningBo.CreateInspector(login, nombres, apellidos, identificacion, passwordEncrypt, 4,0, enabledType);

                if (idUser > 0)
                {
                    var sessionFiles = Session["FilesInspector"] as List<UtilityModels>;
                    if (sessionFiles != null)
                    {
                        foreach (var item in Session["FilesInspector"] as List<UtilityModels>)
                        {
                            string path;
                            switch (item.type)
                            {
                                case "Photo":
                                    path = string.Format("~/{0}/Images/{1}", _pathInspector, idUser);
                                    Directory.CreateDirectory(Server.MapPath(path));
                                    using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + idUser.ToString() + item.extension)))
                                    {
                                        file.Write(item.file, 0, item.file.Length);
                                    }
                                    break;
                                case "CourseInitial":
                                    path = string.Format("~/{0}/Courses/{1}/Initial", _pathInspector, idUser);
                                    Directory.CreateDirectory(Server.MapPath(path));
                                    using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + idUser.ToString() + item.extension)))
                                    {
                                        file.Write(item.file, 0, item.file.Length);
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    Session["FilesInspector"] = null;
                }

                return Json(new { result = true, idUser, login }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ProcessCreateRol(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string nombres = collection["name"];
                string apellidos = collection["lastname"];
                string email = collection["email"];
                string login = collection["login"];
                string password = collection["pass"];
                int headQuarterType = collection["headQuarterType"].ToString() != string.Empty ? Convert.ToInt32(collection["headQuarterType"].ToString()) : 0;
                var passwordEncrypt = encryptData.Encrypt(password, true);
                int RoleId = collection["cbxRole"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxRole"].ToString()) : 0;

                var idUser = _trainningBo.CreateUser(login, nombres, apellidos, email, passwordEncrypt, RoleId, headQuarterType);

                return Json(new { result = true, idUser, login }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }



        [HttpPost, ValidateInput(false)]
        public JsonResult ProcessEditPerson(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int idPerson = int.Parse(collection["ID"]);
                string identification = collection["identification"];
                string name = collection["name"];
                string lastName = collection["lastName"];
                int headquarterId = int.Parse(collection["cbxHeadquarter"]);


                _trainningBo.EditPerson(idPerson, headquarterId, name, lastName);
                return Json(new { result = true, idPerson, identification }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }


        public JsonResult ProcessEditUser(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int idUser = int.Parse(collection["ID"]);
                string login = collection["login"];
                string email = collection["email"];
                string password = collection["pass"];
                string passwordEncrypt;
                if (!string.IsNullOrEmpty(password))
                    passwordEncrypt = encryptData.Encrypt(password, true);
                else
                    passwordEncrypt = "";
                int headQuarterType = collection["headQuarterType"].ToString() != string.Empty ? Convert.ToInt32(collection["headQuarterType"].ToString()) : 0;

                int RoleId = collection["cbxRole"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxRole"].ToString()) : 0;



                _trainningBo.EditUser(idUser, login, email, passwordEncrypt, headQuarterType, RoleId);
                return Json(new { result = true, idUser, login }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public JsonResult ProcessEditInspector(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int idUser = int.Parse(collection["ID"]);
                int enabledType = collection["enabledInspector"].ToString() != string.Empty ? Convert.ToInt32(collection["enabledInspector"].ToString()) : 0;
                string login = collection["name"];
                _trainningBo.EditInspector(idUser, enabledType);
                return Json(new { result = true, idUser, login }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ProcessCreateCourse(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int idPerson = int.Parse(collection["idPerson"].ToString());
                string identification = collection["identification"].ToString();
                int courseId = collection["cbxCourse"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCourse"].ToString()) : 0; ;
                DateTime ccourseDate = Convert.ToDateTime(collection["ccourseDate"]);

                var id = _trainningBo.CreateCourse(idPerson, courseId, ccourseDate);
                foreach (var item in Session["FilesUser"] as List<UtilityModels>)
                {
                    string path;
                    switch (item.type)
                    {
                        case "OtherCourse":
                            path = string.Format("~/{0}/Courses/{1}/Courses", _pathPerson, idPerson);
                            Directory.CreateDirectory(Server.MapPath(path));
                            using (Stream file = System.IO.File.OpenWrite(Server.MapPath(path + '/' + identification + '_' + courseId + '_' + id + item.extension)))
                            {
                                file.Write(item.file, 0, item.file.Length);
                            }
                            break;
                        default:
                            break;
                    }

                }
                Session["FilesUser"] = null;
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpPost]
        public ActionResult UploadFileInspector(FormCollection collection)
        {
            try
            {
                var path = String.Empty;

                foreach (string filetmps in Request.Files)
                {
                    var utlityModels = Session["FilesInspector"] as List<UtilityModels>;
                    HttpPostedFileBase filetmp = Request.Files[filetmps];
                    if (filetmp != null)
                    {
                        var b = new BinaryReader(filetmp.InputStream);
                        int length = Convert.ToInt32(filetmp.InputStream.Length);
                        byte[] binaryData = b.ReadBytes(length);
                        var ext = Path.GetExtension(filetmp.FileName);

                        if (utlityModels == null)
                        {
                            var objectSession = new List<UtilityModels>
                            {
                                new UtilityModels{
                                file= binaryData,
                                extension = ext,
                                type=collection["type"]
                                }

                            };
                            Session["FilesInspector"] = objectSession;
                        }
                        else
                        {
                            utlityModels.Add(new UtilityModels
                            {
                                extension = ext,
                                file = binaryData,
                                type = collection["type"]
                            });
                            Session["FilesInspector"] = utlityModels;
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

        [HttpPost]
        public ActionResult UploadFile(FormCollection collection)
        {
            try
            {
                var path = String.Empty;

                foreach (string filetmps in Request.Files)
                {
                    var utlityModels = Session["FilesUser"] as List<UtilityModels>;
                    HttpPostedFileBase filetmp = Request.Files[filetmps];
                    if (filetmp != null)
                    {
                        var b = new BinaryReader(filetmp.InputStream);
                        int length = Convert.ToInt32(filetmp.InputStream.Length);
                        byte[] binaryData = b.ReadBytes(length);
                        var ext = Path.GetExtension(filetmp.FileName);

                        if (utlityModels == null)
                        {
                            var objectSession = new List<UtilityModels>
                            {
                                new UtilityModels{
                                file= binaryData,
                                extension = ext,
                                type=collection["type"]
                                }

                            };
                            Session["FilesUser"] = objectSession;
                        }
                        else
                        {
                            utlityModels.Add(new UtilityModels
                            {
                                extension = ext,
                                file = binaryData,
                                type = collection["type"]
                            });
                            Session["FilesUser"] = utlityModels;


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

        [HttpPost]
        public ActionResult DeleteCoursesById(int id)
        {
            try
            {
                var model = _trainningBo.DeleteCoursesById(id);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                throw;
            }

        }
        #endregion

    }
}
