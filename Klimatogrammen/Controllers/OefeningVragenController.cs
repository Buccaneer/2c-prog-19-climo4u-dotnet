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
            VragenIndexViewModel vraagVM = new VragenIndexViewModel(leerling.Graad.Vragen, leerling.Klimatogram);
            return View(vraagVM);
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, [Bind(Prefix = "Antwoorden")] AntwoordViewModel antwoorden)
        { 

            return View();
        }
    }
}