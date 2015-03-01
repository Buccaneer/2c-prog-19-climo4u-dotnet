using Klimatogrammen.Models.Domein;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.ViewModels
{
    public class VraagViewModel
    {
        public AntwoordViewModel Antw { get; set; }
        public string VraagTekst { get; set; }
        public SelectList Antwoorden { get; set; }

        [Required(ErrorMessage = "Er moet een antwoord geselecteerd worden.")]
        public string Antwoord { get; set; }

        public bool Resultaat { get; set; }

        public string ValidatieTekst { get; set; }

        public VraagViewModel(Vraag vraag, Klimatogram klimatogram)
        {
            VraagTekst = vraag.VraagTekst;
            Antwoorden = new SelectList(vraag.Parameter.GeefMogelijkeAntwoorden(klimatogram));
            
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