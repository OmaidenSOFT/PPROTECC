using LogicBo;
using System;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class IndicadoresController : Controller
    {
        IndicadoresBo _indicadoresBo = new IndicadoresBo();
        HeadquarterBo _headquarterBo = new HeadquarterBo();

        // GET: Indicadores
        public ActionResult Index()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value", "0");

            return PartialView();
        }

        public JsonResult GetInspection1(int headquarterId, int year)
        {
            try
            {
                var result = _indicadoresBo.GetInspectionChar1(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult GetInspection2(int headquarterId, int year)
        {
            try
            {
                var result = _indicadoresBo.GetInspectionChar2(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult GetPerson1(int headquarterId, int year)
        {
            try
            {
                var result = _indicadoresBo.GetPersonChar1(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult GetPerson2(int headquarterId, int year)
        {
            try
            {
                var result = _indicadoresBo.GetPersonChar2(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult GetFinding1(int headquarterId, int year)
        {
            try
            {
                var result = _indicadoresBo.GetFindingChar1(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult GetFinding2(int headquarterId, int year)
        {
            try
            {
                var result = _indicadoresBo.GetFindingChar2(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}