using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Models.Domein
{
    public class VraagNeerslagInDeZomer : Vraag
    {
        public VraagNeerslagInDeZomer(Klimatogram k)
            : base(k)
        {
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            throw new NotImplementedException();
        }

        public override string GeefVraagTekst()
        {
            return "Hoeveelheid neerslag in de zomer? ";
        }

        public override void ValideerVraag(string antwoord)
        {
            throw new NotImplementedException();
        }
    }
}