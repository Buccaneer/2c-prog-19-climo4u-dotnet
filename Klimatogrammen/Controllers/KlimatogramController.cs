using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Klimatogrammen.Controllers
{
    public class KlimatogramController : Controller
    {
        private ISessionRepository _sessionRepository;

        public KlimatogramController(ISessionRepository sessieRepository)
        {
            _sessionRepository = sessieRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContinentIndexViewModel continentIVM)
        {
            // TODO : IMPLEMENT
            return View(continentIVM);
        }

    }
}