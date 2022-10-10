using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class PartnerController : Controller
    {
        //#region Propiedades
        //private readonly LogicBo.PartnerBo _partnerBo = new LogicBo.PartnerBo();
        //#endregion
        //public ActionResult PageInitial()
        //{
        //    var model = _partnerBo.GetAllPartner();
        //    return PartialView(model);
        //}
        //public ActionResult Create()
        //{
        //    return PartialView();
        //}
        //[HttpPost]
        //public JsonResult ProcessCreate(FormCollection collection)
        //{
        //    try
        //    {
        //        var session = Session["SessionUser"] as SessionModels;
        //        if (session == null)
        //            throw new Exception("Se ha perdido la sesión del Usuario");

        //        string name = collection["name"];
        //        string email = collection["email"];
        //        string phone = collection["phone"];
        //        string address = collection["address"];
        //        bool state = collection["state"]=="on";
        //        _partnerBo.SavePartner(name, email, phone, address, state, session.IdUser);
        //        return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
        //        throw;
        //    }
        //}
    }
}
