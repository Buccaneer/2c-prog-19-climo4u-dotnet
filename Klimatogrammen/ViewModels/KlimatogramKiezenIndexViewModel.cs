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
        public SelectList Continenten { get; set; }

        public ContinentIndexViewModel(IEnumerable<Continent> continenten)
        {
            {
                Continenten = new SelectList(continenten, "Naam");
            }

        }
    }

    public class LandIndexViewModel
    {
        [Required(ErrorMessage = "Land moet gekozen worden")]
        public SelectList Landen { get; set; }
        [Required(ErrorMessage = "Continent moet gekozen worden")]
        public ContinentIndexViewModel ContinentIVM { get; set; }

        public LandIndexViewModel(IEnumerable<Land> landen, ContinentIndexViewModel c)
        {
            {
                ContinentIVM = c;
                Landen = new SelectList(landen, "Naam");
            }

        }
    }

    public class LocatieIndexViewModel
    {
        [Required(ErrorMessage = "Locatie moet gekozen worden")]
        public SelectList Locaties { get; set; }
        [Required(ErrorMessage = "Land moet gekozen worden")]
        public LandIndexViewModel LandIVM { get; set; }

        public LocatieIndexViewModel(IEnumerable<Klimatogram> locaties, LandIndexViewModel l)
        {
            {
                LandIVM = l;
                Locaties = new SelectList(locaties, "Locatie");
            }

        }
    }
}