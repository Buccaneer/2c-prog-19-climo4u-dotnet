﻿using System.Collections;
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

namespace Klimatogrammen.Controllers
{
    public class KlimatogramController : Controller
    {
        private IContinentRepository _klimatogrammenRepository;

        public KlimatogramController(IContinentRepository klimatogrammenRepository)
        {
            _klimatogrammenRepository = klimatogrammenRepository;
        }

        public ActionResult Index(Leerling leerling)
        {
            if (leerling == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KlimatogramKiezenIndexViewModel kIVM = null;
                switch (leerling.Graad)
                {
                    case Graad.Een:
                    {
                        kIVM = new KlimatogramKiezenIndexViewModel(_klimatogrammenRepository.GeefContinent("Europa"));
                        break;
                    }
                    case Graad.Twee:
                    kIVM = new KlimatogramKiezenIndexViewModel(_klimatogrammenRepository.GeefContinenten());
                        break;
                    case Graad.Drie:
                        return RedirectToAction("Index","Home");
                }
            
            return View(kIVM);
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, KlimatogramKiezenIndexViewModel kVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            Continent continent = _klimatogrammenRepository.GeefContinent(kVM.Continent);
       

            if (!continent.Landen.Any())
            {
                TempData["Error"] = "Er zijn geen landen in de databank gevonden voor het geselecteerde continent.";
                return JavaScript("window.location = '" + Url.Action("Index") + "'");
            }
            HttpContext.Session["continent"] = continent;
            return PartialView("_KiesLand", new KlimatogramKiezenLandViewModel(continent.Landen));
        }

        [HttpPost]
        public ActionResult KiesLand(Leerling leerling, Continent continent, KlimatogramKiezenLandViewModel kVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            Land land = continent.Landen.FirstOrDefault(l => l.Naam.Equals(kVM.Land));
      
            if (kVM.Land == null || !land.Klimatogrammen.Any())
            {
                TempData["Error"] = "Er zijn geen locaties in de databank gevonden voor het geselecteerde land.";
                return JavaScript("window.location = '" + Url.Action("Index") + "'");
            }

                  HttpContext.Session["land"] = land;


            return PartialView("_KiesLocatie", new KlimatogramKiezenLocatieViewModel(land.Klimatogrammen));
        }

        [HttpPost]
        public ActionResult KiesLocatie(Leerling leerling, Land land, KlimatogramKiezenLocatieViewModel kVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            Klimatogram klimatogram = land.Klimatogrammen.FirstOrDefault(l => l.Locatie.Equals(kVM.Locatie));
           
            if (kVM.Locatie == null || klimatogram == null)
            {
                TempData["Error"] = "Er zijn geen klimatogrammen in de databank gevonden voor de geselecteerde locatie.";
                return JavaScript("window.location = '" + Url.Action("Index") + "'");
            }
            HttpContext.Session["klimatogram"] = klimatogram;

            object klim = new { klimatogram.GemiddeldeTemperatuur, klimatogram.GemiddeldeNeerslag, klimatogram.BeginJaar, klimatogram.EindJaar, Land = klimatogram.Land.Naam, klimatogram.Locatie, klimatogram.TotaalGemiddeldeTemperatuur, klimatogram.TotaalNeerslag };
            return Json(klim);
        }
    }
}