using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;

namespace Klimatogrammen.Controllers
{
    public class VraagController : Controller
    {
        // TODO : IMPLEMENT THIS CLASS
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index(VraagRepository vRep)
        {
            return View();
        }

        public ActionResult ValideerVragen(VraagRepository vRep, VragenIndexViewModel vIVM)
        {
            return View();
        }
    }
}