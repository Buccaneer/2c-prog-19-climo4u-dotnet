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
        }

        public override ICollection<string> GeefMogelijkeAntwoorden()
        {
            return Enumerable.Range(1, 12).Select(n => n.ToString()).ToList();
        }

        public override string GeefVraagTekst()
        {
            return "Hoeveel droge maanden zijn er (D)?";
        }

        public override void ValideerVraag(string antwoord)
        {
            var neerslagen = _klimatogram.GemiddeldeNeerslag.ToArray();
            double factor = 2.0;
            int index = 0;
            int aantal = _klimatogram.GemiddeldeTemperatuur.Count(t => neerslagen[index++].Waarde / factor <= t.Waarde);

            if (antwoord.Equals(aantal.ToString()))
            {
                Resultaat = Resultaat.Juist;
                _antwoord = aantal.ToString();
            }
            else
            {
                Resultaat = Resultaat.Fout;
                _antwoord = antwoord;
            }
        }
    }
}