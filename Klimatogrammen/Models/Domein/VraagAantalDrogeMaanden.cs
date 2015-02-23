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
            ICollection<string> mogelijkeAntwoorden = new Collection<string>();
            int o = 1;

            for (int i = 0; i < 12; i++)
            {
                mogelijkeAntwoorden.Add(o.ToString());
                o++;
            }
            return mogelijkeAntwoorden;
        }

        public override string GeefVraagTekst()
        {
            return "Hoeveel droge maanden zijn er (D)? ";
        }

        public override void ValideerVraag(string antwoord)
        {
            int aantal = 0;
            foreach (Neerslag n in _klimatogram.GemiddeldeNeerslag)
            {
               //aanvullen
            }
        }
    }
}