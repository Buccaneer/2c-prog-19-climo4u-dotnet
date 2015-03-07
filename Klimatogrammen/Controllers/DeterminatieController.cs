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
            DeterminatieTabel tabel = leerling.Graad.DeterminatieTabel;
            ResultaatBlad knoop = tabel.Determineer(leerling.Klimatogram);
            DeterminatieIndexViewModel determinatieIndexViewModel = new DeterminatieIndexViewModel(knoop);
            return View(determinatieIndexViewModel);
        }
    }
}