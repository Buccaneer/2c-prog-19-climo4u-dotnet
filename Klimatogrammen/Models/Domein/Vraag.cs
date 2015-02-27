using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public class Vraag
    {

        public Resultaat Resultaat { get; set; }

        public Parameter Parameter { get; set; }

        public string VraagTekst { get; set; }

        public void ValideerVraag(string antwoord, Klimatogram klimatogram)
        {
            Resultaat = Parameter.BerekenWaarde(klimatogram).ToString().Equals(antwoord)
                ? Resultaat = Resultaat.Juist
                : Resultaat = Resultaat.Fout;
        }
    }
}
