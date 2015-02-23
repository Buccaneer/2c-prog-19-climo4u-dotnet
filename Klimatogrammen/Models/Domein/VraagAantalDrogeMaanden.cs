using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Models.Domein
{
    public class VraagAantalDrogeMaanden : Vraag
    {
        public VraagAantalDrogeMaanden(Klimatogram k)
            : base(k)
        {
            var neerslagen = _klimatogram.GemiddeldeNeerslag.ToArray();
            double factor = 2.0;
            int index = 0;
            int aantal = _klimatogram.GemiddeldeTemperatuur.Count(t => neerslagen[index++].Waarde / factor <= t.Waarde);
            _antwoord = aantal.ToString();
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            return Enumerable.Range(1, 12).Select(n => n.ToString()).ToList();
        }

        public override string GeefVraagTekst()
        {
            return "Hoeveel droge maanden zijn er (D)? ";
        }

        public override void ValideerVraag(string antwoord) {
            Resultaat = _antwoord.Equals(antwoord) ? Resultaat.Juist : Resultaat.Fout;
        }
    }
}