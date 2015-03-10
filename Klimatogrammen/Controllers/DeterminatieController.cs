﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;

namespace Klimatogrammen.Controllers
{
    public class DeterminatieController : Controller
    {
        // GET: Determinatie
        public ActionResult Index(Leerling leerling)
        {
            ActionResult route = RedirectIndienNodig(leerling);
            if (route != null)
                return route;
            return View(new DeterminatieIndexViewModel());
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, DeterminatieIndexViewModel determinatieVM)
        {
            DeterminatieTabel tabel = leerling.Graad.DeterminatieTabel;
            determinatieVM.Antwoord = tabel.Determineer(leerling.Klimatogram).DeterminatieKnoopId;
            determinatieVM.Correct = determinatieVM.GebruikersAntwoord.Equals(determinatieVM.Antwoord);
            if (determinatieVM.Correct)
            {
                if (leerling.Graad.Nummer == 1)
                {
                    determinatieVM.PartialViewName = "_Graad1";
                    determinatieVM.AntwoordVM = new VegetatieAntwoordViewModel(tabel.VegetatieType.Naam,tabel.VegetatieType.Foto);
                }
                else
                {
                    determinatieVM.VraagVM = new VegetatieVraagViewModel(leerling, tabel.VegetatieType.Foto);
                    determinatieVM.PartialViewName = leerling.Graad.Jaar == 1 ? "_Graad2jaar1" : "_Graad2jaar2";
                }
            }
            return View(determinatieVM);
        }

        [HttpPost]
        public ActionResult VerbeterVraagGraad2(Leerling leerling, DeterminatieIndexViewModel determinatieVM)
        {
            DeterminatieTabel tabel = leerling.Graad.DeterminatieTabel;
            if (determinatieVM.VraagVM.GebruikersAntwoord.Equals(tabel.VegetatieType.Naam))
            {
                determinatieVM.VraagVM.Correct = true;
            }
            return View("Index", determinatieVM);
        }

        public ActionResult GetJSON(Leerling leerling)
        {
            DeterminatieTabel tabel = leerling.Graad.DeterminatieTabel;
            object tbl = tabel.MaakJsonObject();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }

        private ActionResult RedirectIndienNodig(Leerling leerling)
        {
            if (leerling == null || leerling.Graad == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (leerling.Graad.Nummer == 3)
            {
                //TODO: controller voor kaartje bestaat nog niet dus nu naar home
                return RedirectToAction("Index", "Home");
            }
            if (leerling.Klimatogram == null)
            {
                return RedirectToAction("Index", "Klimatogram");
            }
            return null;
        }


    }
}