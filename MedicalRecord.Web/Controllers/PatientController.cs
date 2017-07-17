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

        public ActionResult Tooth()
        {
            return PartialView();
        }

        public ActionResult Disease()
        {
            return PartialView();
        }

        public ActionResult RemoveDisease(string disease)
        {
            // delete disease
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}