﻿using LogicBo;
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
    public class IzageController : Controller
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
        Util util;
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
        public ActionResult DownloadResult()
        {
            return PartialView();
        }
        public ActionResult CreateEquipmentIzage()
        {
            ViewBag.HeadquarterDictionary = new SelectList(_headquarterBo.GetDictionary(), "Key", "Value");
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
        public PartialViewResult AssignEquipment(string tag, int sedeid)
        {
            ViewBag.tag = tag;
            ViewBag.sedeid = sedeid;
            return PartialView();
        }

    }
}
