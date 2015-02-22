using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public abstract class Vraag
    {
        protected Klimatogram _klimatogram;
        protected string _antwoord;

        public Vraag(Klimatogram klimatogram)
        {
            _klimatogram = klimatogram;
        }

        public Resultaat Resultaat { get; private set; }

        public Maand Maand { get; private set; }

        public abstract ICollection<string> GeefMogelijkeAntwoorden();

        public abstract string GeefVraagTekst();


        public string GeefValidatieTekst()
        {
            return ""; // TODO : Implement
            //suggestie : switch case Resultaat.Juist en Resultaat.Fout,
            //return _antwoord + " is correct." of _antwoord + " is fout."
            //zie analoog met de testen VraagWarmsteMaandGeeftValidatieTekstBijJuist(),
            // VraagWarmsteMaandGeeftValidatieTekstBijFout() etc
        }
        public abstract void ValideerVraag(string antwoord);
    }
}
