using LogicBo;
using System;
using System.Data;
using System.Web;
using System.Web.Mvc;
using Utils;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Access]
    public class FactoresIzajeController : Controller
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
        IzageBo _izageBo = new IzageBo();
        FactoresIzajeBo _factoresIzajeBo = new FactoresIzajeBo();
        CategoriaIzajeBo _categoriaIzajeBo = new CategoriaIzajeBo();
        Util util;
        #endregion
        public ActionResult Index()
        {
            ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value");
            ViewBag.GetCategoriaDictionary = new SelectList(_factoresIzajeBo.GetCategoriaDictionary(), "Key", "Value");

            DataSet ds = new DataSet();

            var result = _categoriaIzajeBo.GetInfo();
            ds.Tables.Add(result);
            result = _factoresIzajeBo.GetInfo();
            ds.Tables.Add(result);

            return PartialView(ds);
        }

        public PartialViewResult IndexCategoriasIzaje()
        {

            ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value");
            var result = _categoriaIzajeBo.GetInfo();

            return PartialView(result);
        }
        public PartialViewResult IndexFactoresIzaje()
        {
            ViewBag.GetCategoriaDictionary = new SelectList(_factoresIzajeBo.GetCategoriaDictionary(), "Key", "Value");
            var result = _factoresIzajeBo.GetInfo();

            return PartialView(result);
        }

        public PartialViewResult CreateCategoriaIzaje(int id)
        {
            ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value");

            return PartialView();
        }

        public PartialViewResult CreateFactorIzaje(int id)
        {
            ViewBag.GetCategoriaDictionary = new SelectList(_factoresIzajeBo.GetCategoriaDictionary(), "Key", "Value");

            return PartialView();
        }

        public PartialViewResult SearchFactoresIzaje(FormCollection collection)
        {
            try
            {

                var result = _factoresIzajeBo.GetInfo();

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartialViewResult SearchCategoriasIzaje(FormCollection collection)
        {
            try
            {

                var result = _categoriaIzajeBo.GetInfo();

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult CreateFactoresIzaje()
        {
            ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value");
            ViewBag.GetCategoriaDictionary = new SelectList(_factoresIzajeBo.GetCategoriaDictionary(), "Key", "Value");

            return PartialView();
        }

        [HttpPost]
        public JsonResult ProcessCreateFactorIzaje(FormCollection collection, HttpPostedFileBase pic)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");
                int idCategoria = collection["cbxCategoria"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCategoria"].ToString()) : 0;
                string nombre = collection["txbFactor"].ToString();

                _factoresIzajeBo.Create(idCategoria, nombre);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult ProcessCreateCategoriaIzaje(FormCollection collection, HttpPostedFileBase pic)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");
                int idTipoEquipo = collection["cbxTipoEquipo"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxTipoEquipo"].ToString()) : 0;
                string nombre = collection["txbCategoria"].ToString();

                _categoriaIzajeBo.Create(idTipoEquipo, nombre);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public PartialViewResult EditCategoriaIzage(int id)
        {
            try
            {
                var result = _categoriaIzajeBo.GetById(id);

                ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value", result.Rows[0]["IdTipoEquipoFK"].ToString());

                ViewBag.id = id;
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult ProcessEditCategoriaIzage(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int id = Convert.ToInt32(collection["ID"].ToString());
                string nombre = collection["txbCategoria"].ToString();
                int idTipoEquipo = collection["cbxTipoEquipo"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxTipoEquipo"].ToString()) : 0;

                _categoriaIzajeBo.Edit(id, idTipoEquipo, nombre);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public PartialViewResult EditFactorIzage(int id)
        {
            try
            {
                var result = _factoresIzajeBo.GetById(id);

                ViewBag.GetCategoriaDictionary = new SelectList(_factoresIzajeBo.GetCategoriaDictionary(), "Key", "Value", result.Rows[0]["IdCategoriaFK"].ToString());

                ViewBag.id = id;
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult ProcessEditFactorIzage(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                int id = Convert.ToInt32(collection["ID"].ToString());
                string nombre = collection["txbFactor"].ToString();
                int idCategoria = collection["cbxCategoria"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCategoria"].ToString()) : 0;
                var estador = collection["cbxEstado"].ToString();
                bool estado = Convert.ToBoolean(Convert.ToInt32(collection["cbxEstado"].ToString()));

                _factoresIzajeBo.Edit(id, idCategoria, nombre, estado);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

    }
}