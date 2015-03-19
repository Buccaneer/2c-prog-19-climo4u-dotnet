using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Leerling leerling = HttpContext.Session["leerling"] as Leerling;
            if (leerling != null && leerling.Graad != null && leerling.Graad.Vragen != null)
            //leerling.Graad.Vragen.ForEach(v => v.Resultaat = Resultaat.Onbepaald);
            
            HttpContext.Session.RemoveAll();
           
            
            return View();
        }

    }
}