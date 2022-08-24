using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Entity;

namespace WebApplication1.Controllers
{
    public class SavedPersonController : Controller
    {
        //private ModelEntities db = new ModelEntities();
        //private LogicBo.SavedPersonBo _savedPersonBo = new LogicBo.SavedPersonBo();

        //public ActionResult PageInitial()
        //{
        //    return PartialView(_savedPersonBo.GetAllSavedPerson());
        //}
        //public ActionResult Create()
        //{
        //    ViewBag.listCity= new SelectList(_savedPersonBo.GetListCity(), "idCityPk", "name");
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

        //        string identification = collection["identification"];
        //        string name = collection["name"];
        //        string surName = collection["surName"];
        //        string address = collection["address"];
        //        string age = collection["age"];
        //        string phone = collection["phone"];
        //        int city = collection["listCity"].ToString() != string.Empty ? Convert.ToInt32(collection["listCity"].ToString()) : 0;
        //        bool state = collection["state"] == "on";
        //        _savedPersonBo.SaveSavedPerson(identification,name,surName,address,age,phone,city,state, session.IdUser);
        //        return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
        //        throw;
        //    }
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
