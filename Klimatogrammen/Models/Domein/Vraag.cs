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
            throw new System.NotImplementedException();
        }

        public Resultaat Resultaat { get; private set; }

        public abstract ICollection<string> GeefMogelijkeAntwoorden();

        public abstract string GeefVraagTekst();


        public abstract string GeefValidatieTekst();
        public abstract void ValideerVraag(string antwoord);
    }
}
