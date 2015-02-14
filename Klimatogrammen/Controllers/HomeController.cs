using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;

namespace Klimatogrammen.Controllers
{
    public class HomeController : Controller {
        private ISessionRepository _sessionRepository;

        public HomeController(ISessionRepository sessieRepository) {
            _sessionRepository = sessieRepository;
        }

        public ActionResult Index()
        {
            return View(new LeerlingIndexViewModel());
        }

        [HttpPost]
        public ActionResult Index(LeerlingIndexViewModel leerlingIVM) {
            try {
                if (ModelState.IsValid) {
                    Leerling l = new Leerling();
                    l.Graad = leerlingIVM.Graad;
                        l.Jaar = leerlingIVM.Jaar;
                    _sessionRepository["leerling"] = l;
                    return RedirectToAction("Index", "Klimatogram");
                }
            }
            catch (ArgumentException ex) {
                ModelState.AddModelError("",ex.Message);
            }
            return View(leerlingIVM);
        }
    }
}