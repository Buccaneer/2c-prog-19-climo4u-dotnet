using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Models.Domein
{
    public class VraagNeerslagInDeWinter : Vraag
    {
        public VraagNeerslagInDeWinter(Klimatogram k)
            : base(k)
        {
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            throw new NotImplementedException();
        }

        public override string GeefVraagTekst()
        {
            return "Hoeveelheid neerslag in de winter? ";
        }

        public override void ValideerVraag(string antwoord)
        {
            throw new NotImplementedException();
        }
    }
}