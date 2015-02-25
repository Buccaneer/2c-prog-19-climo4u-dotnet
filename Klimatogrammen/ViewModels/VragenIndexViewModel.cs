using Klimatogrammen.Models.Domein;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Klimatogrammen.ViewModels
{
    public class VragenIndexViewModel
    {
        public ICollection<VraagViewModel> VraagViewModels { get; set; }

        public VragenIndexViewModel(VraagRepository vRep)
        {
            VraagViewModels = new List<VraagViewModel>();
            foreach(Vraag v in vRep.Vragen)
            {
                VraagViewModels.Add(new VraagViewModel(v.GeefVraagTekst(), v.GeefMogelijkeAntwoorden()));
            }
        }

    }

    public class VraagViewModel
    {
        public AntwoordViewModel Antw { get; set; }
        public string VraagTekst { get; set; }
        public SelectList Antwoorden { get; set; }

        [Required(ErrorMessage = "Er moet een antwoord geselecteerd worden.")]
        public string Antwoord { get; set; }

        public bool Resultaat { get; set; }

        public string ValidatieTekst { get; set; }

        public VraagViewModel(string vraag, ICollection<string> antwoorden)
        {
            Antwoord = "1";
            VraagTekst = vraag;
            Antwoorden = new SelectList(antwoorden);
            
        }

        public VraagViewModel()
        {

        }

    }

    public class AntwoordViewModel
    {
        public String Antwoord { get; set; }

        public AntwoordViewModel(string antwoord)
        {
            Antwoord = antwoord;
        }
    }
}