﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Controllers
{
    public class OefeningVragenController : Controller
    {
        public ActionResult Index(Leerling leerling)
        {
            ActionResult route = RedirectIndienNodig(leerling);
            if (route != null)
                return route;
            VragenIndexViewModel vraagVM = new VragenIndexViewModel(leerling.GeefVragen(), leerling.Klimatogram);
            vraagVM.AllesJuist = false;
            return View(vraagVM);
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, [Bind(Prefix = "Antwoorden")] AntwoordViewModel antwoorden)
        {
            ActionResult route = RedirectIndienNodig(leerling);
            if (route != null)
                return route;

            string[] antwden = leerling.ValideerVragen(antwoorden.Antwoord);

            AntwoordViewModel antw = new AntwoordViewModel(antwden);
            VragenIndexViewModel vraagVM = new VragenIndexViewModel(leerling.GeefVragen(), leerling.Klimatogram) { Antwoorden = antw };
            int index = 0;
            vraagVM.AllesJuist =
                leerling.Graad.Vragen.All(v =>
                {
                    var res = v.ValideerVraag(antwoorden.Antwoord[index], leerling.Klimatogram);
                    var vr = vraagVM.Vragen.ElementAt(index++);
                    switch (res)
                    {
                        case Resultaat.Juist:
                            vr.Resultaat = true;
                            break;
                        case Resultaat.Fout:
                            vr.Resultaat = false;
                            break;
                        default:
                            vr.Resultaat = null;
                            break;
                    }
                    return res == Resultaat.Juist;

                });
            return View(vraagVM);
        }

        public ActionResult GetJSON(Leerling leerling)
        {
            Klimatogram klimatogram = leerling.Klimatogram;
            object klim = klimatogram.MaakJsonObject();
            return Json(klim, JsonRequestBehavior.AllowGet);
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
            if (leerling.Graad.Nummer == 2 && leerling.Klimatogram != null)
            {
                return RedirectToAction("Index", "Determinatie");
            }
            if (leerling.Klimatogram == null || (leerling.Graad.Nummer == 2 && leerling.Klimatogram == null))
            {
                return RedirectToAction("Index", "Klimatogram");
            }
            return null;
        }
    }
}