using LogicBo;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Utils;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonasController : Controller
    {
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
        TrainningBo _trainningBo = new TrainningBo();
        IzageBo _izageBo = new IzageBo();
        PersonBo _personBo = new PersonBo();
        Util util;
        #endregion

        //[HttpPost]
        //public ActionResult ArchivoCurso(HttpPostedFileBase archivoCurso, FormCollection collection)
        //{
        //    //foto
        //    if (archivoCurso != null)
        //    {
        //        int idCurso = collection["cbxUnidad"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxUnidad"].ToString()) : 0;
        //        int idPerson = collection["txbIdPersona"].ToString() != string.Empty ? Convert.ToInt32(collection["txbIdPersona"].ToString()) : 0;
        //        DateTime fechaCursoInicial = Convert.ToDateTime(collection["cFechaInicioCurso"].ToString());
        //        string extension = Path.GetExtension(archivoCurso.FileName);
        //        string path = Server.MapPath("~/Archivo_Cursos/");

        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        archivoCurso.SaveAs(string.Concat(path, "Archivo_", idCurso, extension));

        //        var idCursuDet = _personBo.CreateCursoDet(idPerson, idCurso, fechaCursoInicial);
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult ArchivoPersona(HttpPostedFileBase postedFile, FormCollection collection)
        {
            if (postedFile != null)
            {
                string id = collection["txbId"];
                string extension = Path.GetExtension(postedFile.FileName);
                string path = Server.MapPath("~/Fotos_Personas/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                postedFile.SaveAs(string.Concat(path, "Foto_", id, extension));
                _personBo.EditImg(Convert.ToInt32(id), string.Concat("Foto_", id, extension));

            }
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");

            var result = _personBo.GetIndex("", "", 0, "", "");

            return PartialView(result);
        }

        public PartialViewResult SearchPerson(FormCollection collection)
        {
            try
            {
                string identificaion = collection["txbIdentificacion"];
                string nombre = collection["txbNombre"];
                int idSede = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                string fechaInicio = collection["cFechaInicio"];
                string fechaFin = collection["cFechaFin"];
                var result = _personBo.GetIndex(identificaion, nombre, idSede, fechaInicio, fechaFin);

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult CreatePerson()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.CourseDictionary = new SelectList(_trainningBo.GetCourseDictionary(), "Key", "Value");

            return PartialView();
        }

        [HttpPost]
        public JsonResult ProcessCreatePerson(FormCollection collection, HttpPostedFileBase file)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                var x = file;

                string identificacion = collection["txbIdentificacion"];
                string nombre = collection["txbNombre"];
                string apellido = collection["txbApellido"];
                string telefono = collection["txbTelefono"];
                string celular = collection["txbCelular"];
                string correo = collection["txbCorreo"];
                string asignado = collection["txbAsignado"];
                string observacion = collection["txbObservacion"];
                DateTime fechaCursoInicial = Convert.ToDateTime(collection["cFechaInicioCurso"].ToString());
                bool estado = collection["cbxEstado"].ToString() != string.Empty ? Convert.ToBoolean(Convert.ToInt32(collection["cbxEstado"].ToString())) : true;
                int idSede = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int idCurso = collection["cbxCourse"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCourse"].ToString()) : 0;

                var idPerson = _personBo.Create(identificacion, nombre, apellido, idSede, telefono, celular, correo, asignado, "", observacion, estado);
                var idCursuDet = _personBo.CreateCursoDet(idPerson, idCurso, fechaCursoInicial, "");

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public ActionResult EditPerson(int id)
        {
            var model = _personBo.GetInfo(id);
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value", model.Rows[0]["IdSede"].ToString());
            ViewBag.CourseDictionary = new SelectList(_trainningBo.GetCourseDictionary(), "Key", "Value", model.Rows[0]["IdCurso"].ToString());

            return PartialView(model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult ProcessEditPerson(FormCollection collection, HttpPostedFileBase postedFile)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string identificacion = collection["txbIdentificacion"];
                string nombre = collection["txbNombre"];
                string apellido = collection["txbApellido"];
                string telefono = collection["txbTelefono"];
                string celular = collection["txbCelular"];
                string correo = collection["txbCorreo"];
                string asignado = collection["txbAsignado"];
                string observacion = collection["txbObservacion"];
                DateTime fechaCursoInicial = Convert.ToDateTime(collection["cFechaInicioCurso"].ToString());
                bool estado = true;
                int idSede = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int idCurso = collection["cbxCourse"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCourse"].ToString()) : 0;
                int id = collection["ID"].ToString() != string.Empty ? Convert.ToInt32(collection["ID"].ToString()) : 0;

                _personBo.Edit(id, identificacion, nombre, apellido, idSede, telefono, celular, correo, asignado, "", observacion, estado, "");

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public ActionResult DetalleCurso(int id)
        {
            ViewBag.CourseDictionary = new SelectList(_trainningBo.GetCourseDictionary(), "Key", "Value");

            var model = _personBo.GetInfo(id);

            return PartialView(model);
        }

        //[HttpPost]
        public void SearchDetalleCurso(FormCollection collection, HttpPostedFileBase archivoCurso)
        {
            var x = archivoCurso;

            ViewBag.CourseDictionary = new SelectList(_trainningBo.GetCourseDictionary(), "Key", "Value");
            int idCurso = collection["cbxCourse"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCourse"].ToString()) : 0;
            int idPerson = collection["txbIdPersona"].ToString() != string.Empty ? Convert.ToInt32(collection["txbIdPersona"].ToString()) : 0;
            DateTime fechaCursoInicial = Convert.ToDateTime(collection["cFechaInicioCurso"].ToString());

            if (archivoCurso != null)
            {
                string extension = Path.GetExtension(archivoCurso.FileName);
                string path = Server.MapPath("~/Archivo_Cursos/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                archivoCurso.SaveAs(string.Concat(path, "Archivo_", idCurso, extension));
                var idCursuDet = _personBo.CreateCursoDet(idPerson, idCurso, fechaCursoInicial, string.Concat("Archivo_", idCurso, extension));
            }

            var model = _personBo.GetInfo(idPerson);
            Response.Redirect("/pprotecc/");
            Url.Action("Index", "Personas");
        }

        public ActionResult DeleteCurso(int id, int IdGlobal)
        {
            _personBo.DeleteCursoDet(id);
            var model = _personBo.GetInfo(IdGlobal);
            ViewBag.CourseDictionary = new SelectList(_trainningBo.GetCourseDictionary(), "Key", "Value");

            return PartialView("DetalleCurso", model);
        }

    }
}