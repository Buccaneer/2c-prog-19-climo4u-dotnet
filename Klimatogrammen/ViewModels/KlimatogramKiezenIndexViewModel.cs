using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class ContinentIndexViewModel
    {
        [Required(ErrorMessage = "Continent moet gekozen worden")]
        public SelectList Continent { get; set; }

        public ContinentIndexViewModel(IEnumerable<Continent> continenten)
        {
            {
                Continent = new SelectList(continenten, "Naam");
            }

        }
    }

    public class LandIndexViewModel
    {
        [Required(ErrorMessage = "Land moet gekozen worden")]
        public SelectList Land { get; set; }
        [Required(ErrorMessage = "Continent moet gekozen worden")]
        public ContinentIndexViewModel ContinentVm { get; set; }

        public LandIndexViewModel(IEnumerable<Land> landen, ContinentIndexViewModel c)
        {
            {
                ContinentVm = c;
                Land = new SelectList(landen, "Naam");
            }

        }
    }

    public class LocatieIndexViewModel
    {
        [Required(ErrorMessage = "Locatie moet gekozen worden")]
        public SelectList Locatie { get; set; }
        [Required(ErrorMessage = "Land moet gekozen worden")]
        public LandIndexViewModel LandVm { get; set; }

        public LocatieIndexViewModel(IEnumerable<Klimatogram> locaties, LandIndexViewModel l)
        {
            {
                LandVm = l;
                Locatie = new SelectList(locaties, "Locatie");
            }

        }
    }
}