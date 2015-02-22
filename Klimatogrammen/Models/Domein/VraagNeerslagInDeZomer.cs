using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public override void ValideerVraag(string antwoord)
        {
            throw new NotImplementedException();
        }
    }
}