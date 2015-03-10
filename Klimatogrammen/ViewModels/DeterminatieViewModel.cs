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

        public VegetatieVraagViewModel VraagVM { get; set; }

        public VegetatieAntwoordViewModel AntwoordVM { get; set; }
        public string PartialViewName { get; set; }
        public DeterminatieIndexViewModel()
        {
            PartialViewName = null;
        }

    }

    public class VegetatieVraagViewModel
    {
        public SelectList Antwoorden { get; set; }
        public string GebruikersAntwoord { get; set; }
        public bool Correct { get; set; }
        public string Foto { get; set; }

        public VegetatieVraagViewModel(Leerling leerling, string foto)
        {
            Antwoorden = new SelectList(leerling.Graad.DeterminatieTabel.AlleVegetatieTypes.Select(s => s.Naam));
            Foto = foto;
        }

    }

    public class VegetatieAntwoordViewModel
    {
        public string Antwoord { get; set; }
        public string Foto { get; set; }

        public VegetatieAntwoordViewModel(string antwoord, string foto)
        {
            Antwoord = antwoord;
            Foto = foto;
        }

    }

}