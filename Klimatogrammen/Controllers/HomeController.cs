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
        private IGraadRepository _graadRepository;

        public HomeController(ISessionRepository sessieRepository, IGraadRepository graadRepitory) {
            _sessionRepository = sessieRepository;
            _graadRepository = graadRepitory;
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

                    Graad graad = _graadRepository.GeefGraad((int) leerlingIVM.Graad, leerlingIVM.Jaar == null ? 0:(int) leerlingIVM.Jaar);
                    l.Graad = graad;
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