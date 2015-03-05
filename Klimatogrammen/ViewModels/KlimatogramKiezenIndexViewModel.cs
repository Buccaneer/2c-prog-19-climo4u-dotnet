using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class KlimatogramKiezenIndexViewModel
    {
        public SelectList Continenten { get; set; }

        [Required(ErrorMessage = "Er moet een continent geselecteerd worden.")]
        public string Continent { get; set; }

        public KlimatogramKiezenIndexViewModel(IEnumerable<Continent> continenten)
        {
            Continenten = new SelectList(continenten, "Naam", "Naam");
        }

        public KlimatogramKiezenIndexViewModel(Continent continent)
        {
            IEnumerable<Continent> c = new Collection<Continent> {continent};
            Continenten = new SelectList(c, "Naam","Naam");
        }

        public KlimatogramKiezenIndexViewModel()
        {

        }
    }

    public class KlimatogramKiezenLandViewModel
    {
        [Required(ErrorMessage = "Er moet een land geselecteerd worden.")]
        public string Land { get; set; }
        public SelectList Landen { get; set; }

        public KlimatogramKiezenLandViewModel(IEnumerable<Land> landen)
        {
            Landen = new SelectList(landen, "Naam","Naam");
        }

        public KlimatogramKiezenLandViewModel()
        {
        }
    }

    public class KlimatogramKiezenLocatieViewModel
    {
        [Required(ErrorMessage = "Er moet een locatie geselecteerd worden.")]
        public string Locatie { get; set; }
        public SelectList Locaties { get; set; }

        public KlimatogramKiezenLocatieViewModel(IEnumerable<Klimatogram> locaties)
        {
            Locaties=new SelectList(locaties, "Locatie","Locatie");
        }

        public KlimatogramKiezenLocatieViewModel()
        {
            
        }
    }
}