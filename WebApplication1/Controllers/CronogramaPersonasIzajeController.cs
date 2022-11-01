using LogicBo;
using System;
using System.Web.Mvc;
using Utils;

namespace WebApplication1.Controllers
{
    public class CronogramaPersonasIzajeController : Controller
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
        Util util;
        CronogramaPersonasIzajeBo _cronogramaPersonasIzajeBo = new CronogramaPersonasIzajeBo();
        #endregion
        public ActionResult Index()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");

            var result = _cronogramaPersonasIzajeBo.GetIndex(0, "", 2022);

            return PartialView(result);
        }

        public PartialViewResult SearchCronograma(FormCollection collection)
        {
            try
            {
                int idSede = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int annio = collection["cbxAnnio"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAnnio"].ToString()) : 0;
                string tipoCronograma = collection["cbxTipoCronograma"].ToString() != string.Empty ? collection["cbxTipoCronograma"].ToString() : "";

                var result = _cronogramaPersonasIzajeBo.GetIndex(idSede, tipoCronograma, annio);

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult DetalleCronograma(int id)
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");

            var model = _cronogramaPersonasIzajeBo.GetInfo(id, "", "", 0, "", "");

            return PartialView(model);
        }

        public PartialViewResult SearchDetalleCronograma(FormCollection collection)
        {
            try
            {
                string identificaion = collection["txbIdentificacion"];
                string nombre = collection["txbNombre"];
                int idSede = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                string fechaInicio = collection["cFechaInicio"];
                string fechaFin = collection["cFechaFin"];
                int id = collection["txbId"].ToString() != string.Empty ? Convert.ToInt32(collection["txbId"].ToString()) : 0;

                var result = _cronogramaPersonasIzajeBo.GetInfo(id, identificaion, nombre, idSede, fechaInicio, fechaFin);

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}