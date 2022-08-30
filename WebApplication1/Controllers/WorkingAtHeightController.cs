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
using Utils;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Access]
    public class WorkingAtHeightController : Controller
    {
        #region Properties
        private readonly AccountBo _accountBo = new AccountBo();
        WorkingAtHeightBo _workingAtHeightBo = new WorkingAtHeightBo();
        HeadquarterBo _headquarterBo = new HeadquarterBo();
        CategoryBo _categoryBo = new CategoryBo();
        ElementBo _elementBo = new ElementBo();
        LocationBo _locationBo = new LocationBo();
        MarkBo _markBo = new MarkBo();
        CurriculumBo _curriculumBo = new CurriculumBo();
        TechnicalInformationBo _technicalInformationBo = new TechnicalInformationBo();
        EquipmentBo _equipmentBo = new EquipmentBo();
        Util util;
        IzageBo _izageBo= new IzageBo();
        #endregion
        public ActionResult Stock()
        {

            int countryID = Convert.ToInt32(base.Session["CountryID"]);
            var model = _workingAtHeightBo.GetStock(countryID);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult StockByHeadquarter(int headquarterId)
        {
            ViewBag.id = headquarterId;
            var model = _workingAtHeightBo.GetStockByHeadquarter(headquarterId);
            return PartialView(model);
        }

        public ActionResult Curriculum()
        {
            //var model = _workingAtHeightBo.GetStock();
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.CategoryDictionary = new SelectList(_categoryBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult InspectionCert()
        {
            //var model = _workingAtHeightBo.GetStock();
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.CategoryDictionary = new SelectList(_categoryBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult TechnicalInformation()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.CategoryDictionary = new SelectList(_categoryBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }
        public ActionResult IndexEquipment()
        {
            var result = _equipmentBo.GetIndex();
            return PartialView(result);
        }
        public ActionResult IndexSede()
        {
            var result = _headquarterBo.GetIndex();
            return PartialView(result);
        }
        public ActionResult IndexPais()
        {
            var result = _accountBo.GetCountries();
            return PartialView(result);
        }

        public ActionResult IndexSedeCategoria()
        {
            var result = _accountBo.GetCategories();
            return PartialView(result);
        }

        
        public ActionResult DownloadResult()
        {
            return PartialView();
        }
        public ActionResult CreateEquipment()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.ElementDictionary = new SelectList(_elementBo.GetDictionary(), "Key", "Value");
            ViewBag.LocationDictionary = new SelectList(_locationBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }

        public ActionResult CreateSede()
        {
            base.ViewBag.CountryDictionary = new SelectList(_accountBo.GetDictionary(), "Key", "Value");
            base.ViewBag.CategoryDictionary = new SelectList(_accountBo.GetCategoryDictionary(), "Key", "Value");


            return PartialView();
        }

        public ActionResult CreatePais()
        {
            return PartialView();
        }
        public ActionResult CreateCategoriaSede()
        {
            return PartialView();
        }
        public ActionResult CreateEquipmentIzage()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
            ViewBag.UnidadMedidaDictionary= new SelectList(_izageBo.GetUnidadDictionary(), "Key", "Value");
            ViewBag.ElementDictionary = new SelectList(_elementBo.GetDictionary(), "Key", "Value");
            ViewBag.LocationDictionary = new SelectList(_locationBo.GetDictionary(), "Key", "Value");
            return PartialView();
        }


        #region JsonResult
        [HttpPost]
        public JsonResult GetElementByCategory(int idCategory)
        {
            try
            {
                var result = _elementBo.GetDictionaryByCategory(idCategory);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetMarkByHeadquarterElement(int idHeadquarter, int idElement)
        {
            try
            {
                var result = _markBo.GetDictionaryByHeadquarterElement(idHeadquarter, idElement);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ProcessCreateEquipment(FormCollection collection, HttpPostedFileBase file)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string rFID = collection["rFID"];
                string serial = collection["serial"];
                string model = collection["model"];
                string material = collection["material"];
                DateTime fabricationDate = Convert.ToDateTime(collection["cfabricationDate"]);
                string mark = collection["mark"];
                string lot = collection["lot"];
                int element = collection["cbxElement"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxElement"].ToString()) : 0; ;
                int headquarter = collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                DateTime purchaseDate = Convert.ToDateTime(collection["cpurchaseDate"]);
                int cbxAssigned = collection["cbxAssigned"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAssigned"].ToString()) : 0;
                int? areaId = null;
                int? employedId = null;
                int ubicationID = collection["cbxLocation"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxLocation"].ToString()) : 0;

                if (cbxAssigned == 1)
                {
                    areaId = Convert.ToInt32(collection["cbxFieldAssigned"].ToString());
                }
                else if (cbxAssigned == 2)
                {
                    employedId = Convert.ToInt32(collection["cbxFieldAssigned"].ToString());
                }
                int cbxAssignedDate = collection["cbxAssignedDate"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAssignedDate"].ToString()) : 0;

                var idEquipment = _curriculumBo.Create(rFID, serial, model, material, fabricationDate, mark, lot, element, headquarter, purchaseDate, areaId, employedId, cbxAssignedDate, ubicationID);

                util = new Util();
                util.CreateCurriculum(idEquipment);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }



        [HttpPost]
        public JsonResult ProcessCreatePais(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string pais = collection["pais"];
                var idEquipment = _curriculumBo.CreatePais(pais);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }




        [HttpPost]
        public JsonResult ProcessCreateSede(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");
                

                    
                    


                string nombreSede = collection["sede"];

                int paisid = collection["cbxCountry"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxCountry"].ToString()) : 0; ;
                int sedecategoriaid= collection["cbxSedeCategoria"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxSedeCategoria"].ToString()) : 0; ;
                string codeENEL = collection["codeENEL"];
                string ubicacion = collection["ubicaciuon"];

                var idEquipment = _curriculumBo.CreateSede(nombreSede, paisid, sedecategoriaid, ubicacion, codeENEL);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }




        [HttpPost]
        public JsonResult ProcessCreateCategoriaSede(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string categoria = collection["sedecategoria"];
                var idEquipment = _curriculumBo.CreateSedeCategoria(categoria);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }



        [HttpPost]
        public JsonResult ProcessCreateEquipmentIzage(FormCollection collection, HttpPostedFileBase file)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");
                EquipoIzage equipoI=new EquipoIzage();
                equipoI.Equipo=collection["nombreEquipo"];
                equipoI.Marca = collection["marca"];
                equipoI.Modelo = collection["model"];
                equipoI.Serial=collection["serial"];
                equipoI.Lote = collection["lote"];
                equipoI.Tag = collection["rfid"];
                equipoI.Maxcapacidad = collection["maxCapacidad"];
                equipoI.Unidadcapacidad= collection["cbxUnidad"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxUnidad"].ToString()) : 0;
                equipoI.Accesorio = collection["accesorio"];
                equipoI.Asignadoa = collection["asignadoa"];
                equipoI.FechaCompra=Convert.ToDateTime(collection["cpurchaseDate"]);
                equipoI.FechaFabricacion=Convert.ToDateTime(collection["cfabricationDate"]);
                equipoI.Ubicacion = collection["cbxUbicacion"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxUbicacion"].ToString()) : 0;
                equipoI.Sede= collection["cbxHeadquarter"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxHeadquarter"].ToString()) : 0;
                int fechaInspInicial= collection["cbxAssignedDate"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAssignedDate"].ToString()) : 0;
                if(fechaInspInicial==1)
                    equipoI.FechaInspeccionInicial= Convert.ToDateTime(collection["cpurchaseDate"]);
                else if(fechaInspInicial==2)
                 equipoI.FechaInspeccionInicial= Convert.ToDateTime(collection["cfabricationDate"]);
                equipoI.Observaciones = collection["observacion"];
                
                var idEquipment = _izageBo.Create(equipoI);

                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }



        public ActionResult EditEquipment(string RFID)
        {
            var model = _workingAtHeightBo.GetInfoEquipment(RFID);
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value", model.Rows[0]["Sedeid"].ToString());
            return PartialView(model);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult ProcessEditEquipment(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string RFID = collection["RFID"];
                int areaid = int.Parse(collection["areaid"]); ;
                int empleadoid = int.Parse(collection["empleadoid"]);


                _workingAtHeightBo.EditEquipment(RFID, areaid, empleadoid);
                return Json(new { result = true, RFID, areaid, empleadoid }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }


        [HttpPost]
        public JsonResult ProcessUnsuscribe(FormCollection collection)
        {
            try
            {
                var session = Session["SessionUser"] as SessionModels;
                if (session == null)
                    throw new Exception("Se ha perdido la sesión del Usuario");

                string tag = collection["TAG"];
                string comment = collection["comment"];

                _workingAtHeightBo.Unsuscribe(tag, comment);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }


        [HttpPost]
        public JsonResult GetAreaDictionary()
        {
            try
            {
                var result = _workingAtHeightBo.GetAreaDictionary();
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetResourceByHeadquarter(int idHeadquarter)
        {
            try
            {
                var result = _workingAtHeightBo.GetResourceByHeadquarter(idHeadquarter);
                return Json(new { result = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        [HttpPost]
        public JsonResult AssignEquipmentSave(FormCollection collection)
        {
            try
            {
                string tag = collection["tag"];
                int headquarter = collection["sedeid"].ToString() != string.Empty ? Convert.ToInt32(collection["sedeid"].ToString()) : 0;
                int cbxAssigned = collection["cbxAssigned"].ToString() != string.Empty ? Convert.ToInt32(collection["cbxAssigned"].ToString()) : 0;
                int? areaId = null;
                int? employedId = null;
                if (cbxAssigned == 1)
                {
                    areaId = Convert.ToInt32(collection["cbxFieldAssigned"].ToString());
                }
                else if (cbxAssigned == 2)
                {
                    employedId = Convert.ToInt32(collection["cbxFieldAssigned"].ToString());
                }
                var result = _workingAtHeightBo.AssignEquipmentSave(tag, headquarter, areaId, employedId);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        #endregion

        [HttpPost]
        public PartialViewResult SearchCurriculum(FormCollection collection)
        {
            try
            {
                string rFID = string.IsNullOrEmpty(collection["rFID"].ToString()) ? null : collection["rFID"].ToString();
                int tempcbxHeadquarter;
                int tempcbxElement;
                int? cbxHeadquarter = int.TryParse(collection["cbxHeadquarter"].ToString(), out tempcbxHeadquarter) ? tempcbxHeadquarter : (int?)null;
                int? cbxElement = Int32.TryParse(collection["cbxElement"].ToString(), out tempcbxElement) ? tempcbxElement : (int?)null;

                var result = _curriculumBo.GetListByElement(cbxHeadquarter, cbxElement, rFID);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public PartialViewResult SearchInspectionCert(FormCollection collection)
        {
            try
            {
                string rFID = string.IsNullOrEmpty(collection["rFID"].ToString()) ? null : collection["rFID"].ToString();
                int tempcbxHeadquarter;
                int tempcbxElement;
                int? cbxHeadquarter = int.TryParse(collection["cbxHeadquarter"].ToString(), out tempcbxHeadquarter) ? tempcbxHeadquarter : (int?)null;
                int? cbxElement = Int32.TryParse(collection["cbxElement"].ToString(), out tempcbxElement) ? tempcbxElement : (int?)null;

                var result = _curriculumBo.GetListCertificatesByElement(cbxHeadquarter, cbxElement, rFID);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
        [HttpPost]
        public PartialViewResult SearchTechnicalInformation(FormCollection collection)
        {
            try
            {
                int cbxMark = Int32.Parse(collection["cbxMark"].ToString());

                var result = _technicalInformationBo.GetByMark(cbxMark);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public PartialViewResult LoadTechnicalInformation(FormCollection collection)
        {
            try
            {
                int cbxMark = Int32.Parse(collection["cbxMark"].ToString());

                var result = _technicalInformationBo.GetByMark(cbxMark);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public PartialViewResult AssignEquipment(string tag,int sedeid)
        {
            ViewBag.tag = tag;
            ViewBag.sedeid = sedeid;
            return PartialView();
        }

    }
}
