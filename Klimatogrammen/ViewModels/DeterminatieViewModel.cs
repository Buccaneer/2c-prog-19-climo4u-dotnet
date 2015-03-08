using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class DeterminatieIndexViewModel
    {
        public int GebruikersAntwoord { get; set; }
        public int Antwoord { get; set; }
        public bool Correct { get; set; }
        public DeterminatieIndexViewModel()
        {   
        }
    }

    public class VegetatieVraagViewModel
    {
        public SelectList Antwoorden { get; set; }
        public string GebruikersAntwoord { get; set; }
        public string Foto { get; set; }

        public VegetatieVraagViewModel(Leerling leerling)
        {
            Antwoorden = new SelectList(leerling.Graad.DeterminatieTabel.GeefAlleVegetatieTypes());
        }

    }

    public class VegetatieAntwoordViewModel
    {
        public string Antwoord { get; set; }
        public string Foto { get; set; }

    }

}