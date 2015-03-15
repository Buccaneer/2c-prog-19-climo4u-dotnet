using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Controllers
{
    public class LocatieOefeningController : Controller
    {
        //
        // GET: /LocatieOefening/
        public ActionResult Index(Leerling leerling) {
            var items = leerling.GeefKlimatogrammenDerdeGraad();
            return View();
        }
	}
}