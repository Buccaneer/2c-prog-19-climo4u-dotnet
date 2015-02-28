using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;

namespace Klimatogrammen.Controllers
{
    public class OefeningVragenController : Controller
    {
       // TODO : IMPLEMENT THIS CLASS
        public ActionResult Index(Leerling leerling, IEnumerable<Vraag> vragen)
        {
            VragenIndexViewModel vIVM = new VragenIndexViewModel(vragen, leerling.Klimatogram);
            return View(vIVM);
        }

        [HttpPost]
        public ActionResult Index(Leerling leerling, IEnumerable<Vraag> vragen, VragenIndexViewModel vragenIndexViewModel)
        { 
            return View();
        }
    }
}