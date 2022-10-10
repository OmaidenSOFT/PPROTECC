using LogicBo;
using System.Configuration;
using System.Web.Mvc;
using WebApplication1.Filters;
namespace WebApplication1.Controllers
{
    [Access]
    public class HomeController : Controller
    {
        #region Properties
        HomeBo _homeBo = new HomeBo();
        string _pathImge = ConfigurationManager.AppSettings["PathImage"].ToString();
        #endregion
        public ActionResult Index()
        {
            ViewBag.pathImage = _pathImge;
            var model = _homeBo.GetBannerImages(Server.MapPath(_pathImge));
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}