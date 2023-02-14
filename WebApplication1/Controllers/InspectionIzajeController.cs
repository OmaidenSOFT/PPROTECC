using LogicBo;
using System;
using System.Data;
using System.Web.Mvc;
using Utils;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Access]
    public class InspectionIzajeController : Controller
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
        InspectionIzajeBo _inspectionIzajeBo = new InspectionIzajeBo();
        TrainningBo _trainningBO = new TrainningBo();
        ActionResultBo _actionResultBo = new ActionResultBo();
        FinalStateBo _finalStateBo = new FinalStateBo();
        Util util;
        FactoresIzajeBo _factoresIzajeBo = new FactoresIzajeBo();

        #endregion
        // GET: InspectionIzaje


        public ActionResult IndexInspectionIzaje()
        {
            int countryID = Convert.ToInt32(base.Session["CountryID"]);
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionaryWithoutAll(countryID), "Key", "Value");
            var result = _inspectionIzajeBo.GetInfo();

            return PartialView(result);
        }

        public PartialViewResult SearchInspectionIzaje(FormCollection collection)
        {
            try
            {
                var result = _inspectionIzajeBo.GetInfo();

                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult CreateInspectionIzaje(int id, FormCollection collection)
        {
            return PartialView();
        }

        public ActionResult SearchEquipmentByTagSerial(FormCollection collection)
        {
            var model = new DataTable();

            string tagSerial = string.IsNullOrEmpty(collection["txbTagSerial"].ToString()) ? "" : collection["txbTagSerial"].ToString();

            model = _izageBo.GetInfoByTagSerial(tagSerial);

            if (model.Rows.Count > 0)
            {
                int countryID = Convert.ToInt32(base.Session["CountryID"]);
                ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(countryID), "Key", "Value", model.Rows[0]["Sede_id"].ToString());
                ViewBag.UnidadMedidaDictionary = new SelectList(_izageBo.GetUnidadDictionary(), "Key", "Value", model.Rows[0]["UnidadCapacidad_Id"].ToString());
                ViewBag.LocationDictionary = new SelectList(_locationBo.GetUbicacionIzaje(), "Key", "Value", model.Rows[0]["Ubicacion_id"].ToString());

                ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value", model.Rows[0]["IdTipoEquipo"].ToString());
                ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(countryID), "Key", "Value");
                //DISPOSICIÓN GENERAL DE LA INSPECCIÓN
                ViewBag.InspectorDictionary = new SelectList(_trainningBO.GetInspectorDictionary(), "Key", "Value");
                ViewBag.ActionResultDictionary = new SelectList(_actionResultBo.GetDictionary(), "Key", "Value");
                ViewBag.FinalStateDictionary = new SelectList(_finalStateBo.GetStateIzaje(), "Key", "Value");
                return PartialView(model);
            }
            else
            {
                return PartialView("Vacio");
            }
        }

        public ActionResult VisualizarDetalleInspeccion(string tagSerial)
        {
            var model = new DataTable();

            model = _izageBo.GetInfoByTagSerial(tagSerial);
            int countryID = Convert.ToInt32(base.Session["CountryID"]);

            if (model.Rows.Count > 0)
            {
                ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(countryID), "Key", "Value", model.Rows[0]["Sede_id"].ToString());
                ViewBag.UnidadMedidaDictionary = new SelectList(_izageBo.GetUnidadDictionary(), "Key", "Value", model.Rows[0]["UnidadCapacidad_Id"].ToString());
                ViewBag.LocationDictionary = new SelectList(_locationBo.GetUbicacionIzaje(), "Key", "Value", model.Rows[0]["Ubicacion_id"].ToString());

                ViewBag.GetTipoEquipoDictionary = new SelectList(_izageBo.GetTipoEquipoDictionary(), "Key", "Value", model.Rows[0]["IdTipoEquipo"].ToString());
                ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(countryID), "Key", "Value");
                //DISPOSICIÓN GENERAL DE LA INSPECCIÓN
                ViewBag.InspectorDictionary = new SelectList(_trainningBO.GetInspectorDictionary(), "Key", "Value");
                ViewBag.ActionResultDictionary = new SelectList(_actionResultBo.GetDictionary(), "Key", "Value");
                ViewBag.FinalStateDictionary = new SelectList(_finalStateBo.GetStateIzaje(), "Key", "Value");
                return PartialView(model);
            }
            else
            {
                return PartialView("Vacio");
            }
        }

        [HttpPost]
        public JsonResult ProcessCreateInspectionIzaje(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");


                DataTable dt = _factoresIzajeBo.GetByIdEquipo(Convert.ToInt32(collection["idTipoEquipo"].ToString()));



                InspectionIzaje inspectionIzaje = new InspectionIzaje();
                inspectionIzaje.FechaInspeccion = Convert.ToDateTime(collection["cinspectionDate"].ToString());
                inspectionIzaje.IdAccion = collection["cbxActionResult"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxActionResult"].ToString()) : 0;
                inspectionIzaje.IdInspector = collection["cbxInspector"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxInspector"].ToString()) : 0;
                inspectionIzaje.IdEquipo = Convert.ToInt32(collection["id"].ToString());
                inspectionIzaje.IdEstado = collection["cbxFinalState"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxFinalState"].ToString()) : 0;
                inspectionIzaje.Precinto = collection["txbPrecinto"];
                int idInspeccion = _inspectionIzajeBo.Create(inspectionIzaje);

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "Id")
                        {
                            int idFactor = Convert.ToInt32(row["Id"].ToString());

                            _inspectionIzajeBo.CreateDetalle(idFactor, idInspeccion, collection[string.Concat("cbxAssignedDate_", idFactor)].ToString(), collection[string.Concat("txbComentario_", idFactor)].ToString());
                        }
                    }
                }

                util = new Util();
                util.CreateHVIzaje(Convert.ToInt32(collection["id"].ToString()), Server.MapPath("~/Equipo_Izaje/"));
                Response.Redirect("/pprotecc/");
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        public ActionResult EditInspectionIzaje(int id)
        {
            var model = _inspectionIzajeBo.GetInfo();
            int countryID = Convert.ToInt32(base.Session["CountryID"]);
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(countryID), "Key", "Value", model.Rows[0]["Sede_id"].ToString());
            ViewBag.UnidadMedidaDictionary = new SelectList(_izageBo.GetUnidadDictionary(), "Key", "Value", model.Rows[0]["UnidadCapacidad_Id"].ToString());
            ViewBag.LocationDictionary = new SelectList(_locationBo.GetUbicacionIzaje(), "Key", "Value", model.Rows[0]["Ubicacion_id"].ToString());
            return PartialView(model);
        }

    }
}