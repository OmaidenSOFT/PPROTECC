using LogicBo;
using System;
using System.Web.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [Access]
    public class InventarioIzajeController : Controller
    {
        IzageBo _izageBo = new IzageBo();
        PersonBo _PersonBo = new PersonBo();

        public ActionResult Index()
        {
            var result = _izageBo.GetInventario();
            return PartialView(result);
        }

        public ActionResult IndexPersonas()
        {
            var result = _PersonBo.GetInventarioPersonas();
            return PartialView(result);
        }

        public PartialViewResult SearchEquipoByIdSede(int id)
        {
            try
            {
                var result = _izageBo.GetIndex("", id, "", "", "", "");

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartialViewResult SearchPersonasByIdSede(int id)
        {
            try
            {
                var result = _PersonBo.GetIndex("", "", id, "", "");

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}