using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class KlimatogramKiezenIndexViewModel
    {
        public KlimatogramViewModel Klimatogram { get; set; }
        public SelectList Continenten { get; set; }
        public SelectList Landen { get; set; }
        public SelectList Locaties { get; set; }

        public object KlimatogramObject { get; set; }

        public KlimatogramKiezenIndexViewModel(IEnumerable<Continent> continenten, IEnumerable<Land> landen, IEnumerable<Klimatogram> locaties)
        {
            Continenten = new SelectList(continenten, "Naam", "Naam");
            if (!landen.Any())
                Landen = null;
            else
            {
                Landen = new SelectList(landen, "Naam", "Naam");
            }

            if (!locaties.Any())
                Locaties = null;
            else
                Locaties = new SelectList(locaties, "Locatie", "Locatie");
        }

        public KlimatogramKiezenIndexViewModel(IEnumerable<Continent> continenten, IEnumerable<Land> landen)
        {
            Continenten = new SelectList(continenten, "Naam", "Naam");
            Landen = new SelectList(landen, "Naam", "Naam");
            Locaties = null;
        }

        public KlimatogramKiezenIndexViewModel(IEnumerable<Continent> continenten)
        {
            Continenten = new SelectList(continenten, "Naam", "Naam");
        }

        public KlimatogramKiezenIndexViewModel()
        {

        }
    }

    public class KlimatogramViewModel
    {
        [Required(ErrorMessage = "U moet een continent selecteren")]
        public String Continent { get; set; }
        [Required(ErrorMessage = "U moet een land selecteren")]
        public String Land { get; set; }
        [Required(ErrorMessage = "U moet een locatie selecteren")]
        public String Locatie { get; set; }


    }
}