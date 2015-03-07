using System;
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
            return View(new DeterminatieIndexViewModel());
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, DeterminatieIndexViewModel determinatieVM)
        {
            DeterminatieTabel tabel = leerling.Graad.DeterminatieTabel;
            determinatieVM.Correct = determinatieVM.GebruikersAntwoord.Equals(tabel.Determineer(leerling.Klimatogram).KlimaatType);
            if (determinatieVM.Correct)
            {
                if (leerling.Graad.Nummer == 1)
                {
                    //vegetatietype + foto
                }
                else
                {
                    if (leerling.Graad.Jaar == 1)
                    {
                        //vraag vegetatietype + foto
                    }
                    else
                    {
                        //vraag vegetatietype
                    }
                }
            }
            return View(determinatieVM);
        }

        public ActionResult GetJSON(Leerling leerling)
        {
            DeterminatieTabel tabel = leerling.Graad.DeterminatieTabel;
            object tbl = tabel.MaakJsonObject();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }


    }
}