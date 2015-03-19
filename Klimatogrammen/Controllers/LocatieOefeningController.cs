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
        public ActionResult Index(Leerling leerling)
        {
            ActionResult route = RedirectIndienNodig(leerling);
            if (route != null)
                return route;
            leerling.GeefKlimatogrammenDerdeGraad();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, string[] klimatogrammen, string[] locaties)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    leerling.ValideerLocaties(locaties, klimatogrammen);
                    if (leerling.FoutieveKlimatogrammenDerdeJaar.Count > 0)
                    {
                        ViewBag.IsPost = true;
                        TempData["error"] =
                            String.Format("Je had {0} fout(en)! Je kan maar verder totdat alles juist is.",
                                leerling.FoutieveKlimatogrammenDerdeJaar.Count);
                        return View();
                    }
                    return View("VegetatieTypes", new OefeningLocatieVegTypesIndexViewModel(leerling));
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception.Message);
                }
            }
            return View();

        }



        [HttpPost]
        public ActionResult VerbeterVegetatieVragen(Leerling leerling, [Bind(Prefix = "Antwoorden")] AntwoordViewModel antwoorden)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var vm = new OefeningLocatieVegTypesIndexViewModel(leerling);
                    int index = 0;
                    int juist = 0;
                    var klimatogrammen = leerling.GeefKlimatogrammenDerdeGraad().ToList();
                    foreach (var vraag in vm.Vragen)
                    {
                        var klimatogram = klimatogrammen[index];
                        var antwoord = antwoorden.Antwoord[index];
                        var res = leerling.Graad.DeterminatieTabel.Determineer(klimatogram);
                        vraag.Correct = res.VegetatieType.Naam.Equals(antwoord);
                        if (vraag.Correct.Value)
                            juist++;
                        index++;
                    }
                    vm.AllesJuist = juist == antwoorden.Antwoord.Length;
                    vm.Antwoorden = antwoorden;
                    return View("VegetatieTypes", vm);

                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception.Message);
                }
            }
            return View();
        }



        public JsonResult GeefFoutieveKlimatogrammen(Leerling leerling)
        {

            var lijst = leerling.FoutieveKlimatogrammenDerdeJaar.Select(k => k.MaakJsonObject()).ToList();

            return Json(lijst, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GeefKlimatogrammen(Leerling leerling)
        {
            var data = leerling.GeefKlimatogrammenDerdeGraad();

            var lijst = data.Select(k => k.MaakJsonObject()).ToList();

            return Json(lijst, JsonRequestBehavior.AllowGet);
        }

        private ActionResult RedirectIndienNodig(Leerling leerling)
        {
            if (leerling == null || leerling.Graad == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (leerling.Graad.Nummer == 1 && leerling.Klimatogram != null)
            {
                return RedirectToAction("Index", "OefeningVragen");
            }
            if (leerling.Graad.Nummer == 2 && leerling.Klimatogram != null)
            {
                return RedirectToAction("Index", "Determinatie");
            }

            return null;
        }
    }
}