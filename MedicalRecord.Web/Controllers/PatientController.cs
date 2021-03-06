﻿using log4net;
using MedicalRecord.Web.DataPersisters;
using MedicalRecord.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MedicalRecord.Web.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Patient
        [HttpGet]
        public ActionResult Index()
        {
            return View(new PatientModel());
        }

        // GET: Patient
        [HttpGet]
        public ActionResult GetById(int id)
        {
            PatientPersister patientPersister = new PatientPersister();
            var patient = patientPersister.Get(id);
            return View(nameof(Index), patient);
        }

        [HttpPost]
        public ActionResult Index(string patientJson)
        {
            PatientModel patient = JsonConvert.DeserializeObject<PatientModel>(patientJson);
            PatientPersister patientPersister = new PatientPersister();
            try
            {
                if (ModelState.IsValid)
                {
                    int id = patientPersister.CreatePatient(patient);
                    return RedirectToAction(nameof(GetById), new { id = id });
                }

                ModelState.AddModelError("InvalidData", "Намерени са невалидни полета.");
            }
            catch (Exception ex)
            {
                log.Error("Create patient", ex);
                ModelState.AddModelError("Error", ex);
            }

            return View(patient);
        }

        [HttpPost]
        public ActionResult Save(string patientJson)
        {
            PatientModel patient = JsonConvert.DeserializeObject<PatientModel>(patientJson);
            PatientPersister patientPersister = new PatientPersister();
            try
            {
                if (ModelState.IsValid)
                {
                    patientPersister.Save(patient);
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }

                ModelState.AddModelError("InvalidData", "Намерени са невалидни полета.");
            }
            catch (Exception ex)
            {
                log.Error("Create patient", ex);
                ModelState.AddModelError("Error", ex);
            }

            return View(nameof(Index), patient);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            PatientPersister patientPersister = new PatientPersister();
            patientPersister.DeletePatient(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Tooth()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Disease(string newDisease)
        {
            var disease = new DiseaseModel() { Name = newDisease };
            return PartialView(disease);
        }

        [HttpPost]
        public ActionResult Disease(string newDisease, int patientId)
        {
            DiseasePersister diseasePersister = new DiseasePersister();
            var disease = diseasePersister.AddDisease(newDisease, patientId);
            return PartialView(disease);
        }

        [HttpPost]
        public ActionResult RemoveDisease(int diseaseId, int patientId)
        {
            DiseasePersister diseasePersister = new DiseasePersister();
            diseasePersister.RemoveDisease(diseaseId, patientId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult SearchOptions()
        {
            PatientPersister patientPersister = new PatientPersister();
            var searchOptions = patientPersister.GetPatientEGNs();
            return Json(searchOptions);
        }

        public ActionResult RemoveProcedure(int procedureId)
        {
            ToothPersister toothPersister = new ToothPersister();
            toothPersister.RemoveProcedure(procedureId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}