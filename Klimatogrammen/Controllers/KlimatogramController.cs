using System.Collections;
using System.Data.Entity;
using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.DAL;
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
        private IKlimatogrammenRepository _klimatogrammenRepository;

        public KlimatogramController(ISessionRepository sessieRepository, IKlimatogrammenRepository klimatogrammenRepository)
        {
            _sessionRepository = sessieRepository;
            _klimatogrammenRepository = klimatogrammenRepository;
        }

        public ActionResult Index()
        {
            KlimatogramKiezenIndexViewModel kIVM = new KlimatogramKiezenIndexViewModel(_klimatogrammenRepository.GeefContinenten());
            return View(kIVM);
        }

        [HttpPost]
        public ActionResult Index([Bind(Prefix = "Klimatogram")] KlimatogramViewModel k)
        {
            IEnumerable<Continent> continenten = _klimatogrammenRepository.GeefContinenten();
            IEnumerable<Land> landen =
                continenten.Where(cont => cont.Naam.Equals(k.Continent)).SelectMany(cont => cont.Landen);
            IEnumerable<Klimatogram> locatie =
                landen.Where(land => land.Naam.Equals(k.Land)).SelectMany(land => land.Klimatogrammen);

            KlimatogramKiezenIndexViewModel kIVM = new KlimatogramKiezenIndexViewModel(continenten, landen, locatie);
            
            if (k.Continent != null && k.Land != null && k.Locatie != null)
            {
                Klimatogram klimatogram = _klimatogrammenRepository.GeefContinent(k.Continent).Landen.SelectMany(l => l.Klimatogrammen).FirstOrDefault(kl => kl.Locatie.Equals(k.Locatie));
                object klim = new { klimatogram.GemiddeldeTemperatuur, klimatogram.GemiddeldeNeerslag, klimatogram.BeginJaar, klimatogram.EindJaar, Land = klimatogram.Land.Naam, klimatogram.Locatie, klimatogram.TotaalGemiddeldeTemperatuur, klimatogram.TotaalNeerslag };
                kIVM.KlimatogramObject = klim;
            }

            return View(kIVM);
        }


    }
}