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
        // GET: Patient
        public ActionResult Index()
        {
            return View(new PatientModel());
        }

        // GET: Patient
        public ActionResult Index(int id)
        {
            PatientPersister patientPersister = new PatientPersister();
            var patient = patientPersister.Get(id);
            return View(patient);
        }

        [HttpPost]
        public ActionResult Index(PatientModel patient)
        {
            PatientPersister patientPersister = new PatientPersister();
            try
            {
                if (ModelState.IsValid)
                {
                    int id = patientPersister.Save(patient);
                    return RedirectToAction(nameof(Index), new { id = id });
                }
                
                ModelState.AddModelError("InvalidData", "Намерени са невалидни полета.");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            return View(patient);
        }

        public ActionResult Tooth()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Disease(string newDisease)
        {

            return PartialView();
        }

        public ActionResult RemoveDisease(string disease, int patientId)
        {
            DiseasePersister diseasePersister = new DiseasePersister();
            diseasePersister.RemoveDisease(disease, patientId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}