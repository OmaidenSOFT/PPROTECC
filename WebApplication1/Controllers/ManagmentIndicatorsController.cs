using LogicBo;
using System;
using System.Web.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [Access]
    public class ManagmentIndicatorsController : Controller
    {

        #region Properties
        ManagmentIndicatorsBo _managmentIndicatorsBo = new ManagmentIndicatorsBo();
        #endregion
        public ActionResult PageInitial()
        {
            return PartialView();
        }

        public JsonResult GetStock(int headquarterId, int year)
        {
            try
            {
                var result = _managmentIndicatorsBo.GetStock(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        public JsonResult GetAims(int headquarterId, int year)
        {
            try
            {
                var result = _managmentIndicatorsBo.GetAims(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public JsonResult GetCoverage(int headquarterId, int year)
        {
            try
            {
                var result = _managmentIndicatorsBo.GetCoverage(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public JsonResult GetAimsTrainning(int headquarterId, int year)
        {
            try
            {
                var result = _managmentIndicatorsBo.GetAimsTrainning(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public JsonResult GetFindingsByMonth(int headquarterId, int year)
        {
            try
            {
                var result = _managmentIndicatorsBo.GetFindingsByMonth(headquarterId, year);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public JsonResult GetFindingsTotal(int headquarterId, int year)
        {
            try
            {
                var result = _managmentIndicatorsBo.GetFindingsTotal(headquarterId, year);
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