using System.Collections.ObjectModel;
using System.Net.Configuration;
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

    public class VragenIndexViewModel
    {
        public bool AllesJuist { get; set; }
        public ICollection<VraagViewModel> Vragen { get; set; }
        public AntwoordViewModel Antwoorden { get; set; }

        public VragenIndexViewModel(IEnumerable<Vraag> vragen, Klimatogram klimatogram)
        {
            //AllesJuist = vragen.All(v => v.Resultaat == Resultaat.Juist);
            Vragen = new Collection<VraagViewModel>();
            foreach (var vraag in vragen)
            {
                Vragen.Add(new VraagViewModel(vraag, klimatogram));
            }
        }
    }

    public class VraagViewModel
    {
        public string VraagTekst { get; set; }

        public SelectList Antwoorden { get; set; }

        public bool? Resultaat { get; set; }

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
        [Required(ErrorMessage = "Alle antwoorden moeten ingevuld zijn", AllowEmptyStrings = false)]
        public string[] Antwoord { get; set; }

        public AntwoordViewModel(string[] antwoord)
        {
            Antwoord = antwoord;
        }

        public AntwoordViewModel()
        {

        }
    }
}
