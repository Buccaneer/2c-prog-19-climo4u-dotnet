using System.Collections;
using System.Data.Entity;
using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.DAL;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Klimatogrammen.Controllers {
    public class KlimatogramController : Controller {
        private IEnumerable<Continent> _continenten;

        public KlimatogramController(IEnumerable<Continent> continenten) {
            _continenten = continenten;
        }

        public ActionResult Index(Leerling leerling) {
            if (leerling == null) {
                return RedirectToAction("Index", "Home");
            }
            KlimatogramKiezenIndexViewModel kIVM;
            switch (leerling.Graad.Nummer) {
                case 1: {
                        kIVM = new KlimatogramKiezenIndexViewModel(_continenten.First(c => c.Naam.Equals("Europa")));
                        break;
                    }
                case 2:
                    kIVM = new KlimatogramKiezenIndexViewModel(_continenten);
                    break;
                default:
                    return RedirectToAction("Index", "Home");
            }

            return View(kIVM);
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, KlimatogramKiezenIndexViewModel kVM) {
            if (!ModelState.IsValid) {
                return null;
            }

            Continent continent = _continenten.First(c => c.Naam.Equals(kVM.Continent));


            if (!continent.Landen.Any()) {
                TempData["Error"] = "Er zijn geen landen in de databank gevonden voor het geselecteerde continent.";
                return JavaScript("window.location = '" + Url.Action("Index") + "'");
            }
            if (HttpContext != null && HttpContext.Session != null)
                HttpContext.Session["continent"] = continent;
            return PartialView("_KiesLand", new KlimatogramKiezenLandViewModel(continent.Landen));
        }

        [HttpPost]
        public ActionResult KiesLand(Leerling leerling, Continent continent, KlimatogramKiezenLandViewModel kVM) {
            if (!ModelState.IsValid) {
                return null;
            }
            Land land = continent.Landen.FirstOrDefault(l => l.Naam.Equals(kVM.Land));

            if (land != null && (kVM.Land == null || !land.Klimatogrammen.Any())) {
                TempData["Error"] = "Er zijn geen locaties in de databank gevonden voor het geselecteerde land.";
                return JavaScript("window.location = '" + Url.Action("Index") + "'");
            }
            if (HttpContext != null && HttpContext.Session != null)
                HttpContext.Session["land"] = land;
            return PartialView("_KiesLocatie", new KlimatogramKiezenLocatieViewModel(land.Klimatogrammen));
        }

        [HttpPost]
        public ActionResult KiesLocatie(Leerling leerling, Land land, KlimatogramKiezenLocatieViewModel kVM) {
            if (!ModelState.IsValid) {
                return null;
            }

            Klimatogram klimatogram = land.Klimatogrammen.FirstOrDefault(l => l.Locatie.Equals(kVM.Locatie));

            if (kVM.Locatie == null || klimatogram == null) {
                TempData["Error"] = "Er zijn geen klimatogrammen in de databank gevonden voor de geselecteerde locatie.";
                return JavaScript("window.location = '" + Url.Action("Index") + "'");
            }
            if (HttpContext != null && HttpContext.Session != null)
                HttpContext.Session["klimatogram"] = klimatogram;
            leerling.Klimatogram = klimatogram;

            object klim = new { GemiddeldeTemperatuur = klimatogram.Maanden.Select(maand => maand.Temperatuur).ToList(),
                                GemiddeldeNeerslag = klimatogram.Maanden.Select(maand => maand.Temperatuur).ToList(),
                                klimatogram.BeginJaar,
                                klimatogram.EindJaar,
                                Land = klimatogram.Land.Naam,
                                klimatogram.Locatie,
                                TotaalGemiddeldeTemperatuur = klimatogram.GeefGemiddeldeTemperatuur(),
                                TotaalNeerslag = klimatogram.GeefTotaleNeerslag()
            };

            return Json(klim);
        }
    }
}