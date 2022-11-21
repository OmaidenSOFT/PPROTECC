using LogicBo;
using System;
using System.Web.Mvc;
using Utils;

namespace WebApplication1.Controllers
{
    public class CronogramaIzajeController : Controller
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
        CronogramaIzajeBo _cronogramaIzajeBo = new CronogramaIzajeBo();
        Util util;
        #endregion

        public ActionResult Index()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value");

            var result = _cronogramaIzajeBo.GetIndex(0, 0, "Annio", 2022);

            return PartialView(result);
        }

        public PartialViewResult SearchCronograma(FormCollection collection)
        {
            try
            {
                int idSede = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int idTipoEquipo = collection["cbxTipoEquipo"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxTipoEquipo"].ToString()) : 0;
                int annio = collection["cbxAnnio"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAnnio"].ToString()) : 0;
                string tipoCronograma = collection["cbxTipoCronograma"].ToString() != string.Empty ? collection["cbxTipoCronograma"].ToString() : "";

                var result = _cronogramaIzajeBo.GetIndex(idSede, idTipoEquipo, tipoCronograma, annio);

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult DetalleCronograma(int id)
        {
            var model = _cronogramaIzajeBo.GetInfo(id, "", "");

            return PartialView(model);
        }

        public PartialViewResult SearchDetalleCronograma(FormCollection collection)
        {
            try
            {
                string serial = collection["txbSerial"].ToString() != string.Empty ? collection["txbSerial"].ToString() : "";
                string tag = collection["txbTag"].ToString() != string.Empty ? collection["txbTag"].ToString() : "";
                int id = collection["txbId"].ToString() != string.Empty ? Convert.ToInt32(collection["txbId"].ToString()) : 0;

                var result = _cronogramaIzajeBo.GetInfo(id, serial, tag);

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}