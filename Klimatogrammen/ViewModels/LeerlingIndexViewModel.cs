using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class LeerlingIndexViewModel
    {
        public SelectList Graden { get; set; }
        public Graad Graad { get { return (Graad)Graden.SelectedValue; } }
        public int? Jaar { get; set; }

        public LeerlingIndexViewModel()
        {
            Graden = new SelectList(new object[] {
                new {Key=0, Tekst="Eerste graad"}, new {Key=1, Tekst="Tweede graad"}, new {Key=2, Tekst="Derde graad"}
            }, "Key", "Tekst");
        }

        public LeerlingIndexViewModel(Leerling l)
        {
            Graden = new SelectList(new object[] {
                new {Key=0, Tekst="Eerste graad"}, new {Key=1, Tekst="Tweede graad"}, new {Key=2, Tekst="Derde graad"}
            }, "Key", "Tekst");
            Jaar = l.Jaar;
        }
    }
}