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
    public class UsersAndRolesController : Controller
    {
        #region Properties
       
        UsersAndRolesBo _UsersAndRolesBo = new UsersAndRolesBo();
             #endregion
        //public ActionResult UsersAndRoles()
        //{
        //    ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
        //    return PartialView();
        //}
        public ActionResult Certificate()
        {
            return PartialView();
        }
        public ActionResult IndexRoles()
        {
            var result = _UsersAndRolesBo.GetIndexUserAndRol();
            return PartialView(result);
        }
       
       

        #region JsonResult
       

       

        #endregion

    }
}
