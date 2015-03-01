using System;
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
            IEnumerable<VraagViewModel> vraagVM = leerling.Graad.Vragen.Select(vraag => new VraagViewModel(vraag, leerling.Klimatogram));
            return View(vraagVM);
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, IEnumerable<Vraag> vragen, VraagViewModel vragenViewModel)
        { 
            return View();
        }
    }
}