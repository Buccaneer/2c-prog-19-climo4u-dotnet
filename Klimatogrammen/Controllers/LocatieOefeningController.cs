using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;

namespace Klimatogrammen.Controllers
{
    public class LocatieOefeningController : Controller
    {
        //
        // GET: /LocatieOefening/
        public ActionResult Index(Leerling leerling) {
            if (leerling == null)
                return RedirectToAction("Index", "Graad");
            leerling.GeefKlimatogrammenDerdeGraad();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling,  string[] klimatogrammen, string[] locaties) {
            leerling.ValideerLocaties(locaties, klimatogrammen);
            if (leerling.FoutieveKlimatogrammenDerdeJaar.Count > 0) {
                ViewBag.IsPost = true;
                TempData["error"] = String.Format("Je had {0} fout(en)! Je kan maar verder totdat alles juist is.",
                    leerling.FoutieveKlimatogrammenDerdeJaar.Count);
                return View();
            }
            return View("VegetatieTypes", new OefeningLocatieVegTypesIndexViewModel(leerling));
        }

        public JsonResult GeefFoutieveKlimatogrammen(Leerling leerling) {

            var lijst = leerling.FoutieveKlimatogrammenDerdeJaar.Select(k => k.MaakJsonObject()).ToList();

            return Json(lijst, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GeefKlimatogrammen(Leerling leerling) {
            var data = leerling.GeefKlimatogrammenDerdeGraad();

            var lijst = data.Select(k => k.MaakJsonObject()).ToList();

            return Json(lijst, JsonRequestBehavior.AllowGet);
        }
	}
}