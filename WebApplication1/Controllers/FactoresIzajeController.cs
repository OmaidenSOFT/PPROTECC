using LogicBo;
using System;
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
        public ActionResult IndexFactoresIzaje()
        {
            ViewBag.GetCategoriaDictionary = new SelectList(_factoresIzajeBo.GetCategoriaDictionary(), "Key", "Value");
            ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value");

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
        public ActionResult EditFactoresIzaje(int id)
        {
            var model = _izageBo.GetInfo(id);
            int countryID = Convert.ToInt32(base.Session["CountryID"]);
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(countryID), "Key", "Value", model.Rows[0]["Sede_id"].ToString());
            ViewBag.UnidadMedidaDictionary = new SelectList(_izageBo.GetUnidadDictionary(), "Key", "Value", model.Rows[0]["UnidadCapacidad_Id"].ToString());
            ViewBag.LocationDictionary = new SelectList(_locationBo.GetUbicacionIzaje(), "Key", "Value", model.Rows[0]["Ubicacion_id"].ToString());
            return PartialView(model);
        }


    }
}