using System;
using System.Collections.Generic;
using System.Linq;

namespace Klimatogrammen.Models.Domein
{
    public sealed class Maand
    {
        private readonly String _naam;

        public static readonly Maand Januari = new Maand("Januari");
        public static readonly Maand Februari = new Maand("Februari");
        public static readonly Maand Maart = new Maand("Maart");
        public static readonly Maand April = new Maand("April");
        public static readonly Maand Mei = new Maand("Mei");
        public static readonly Maand Juni = new Maand("Juni");
        public static readonly Maand Juli = new Maand("Juli");
        public static readonly Maand Augustus = new Maand("Augustus");
        public static readonly Maand September = new Maand("September");
        public static readonly Maand Oktober = new Maand("Oktober");
        public static readonly Maand November = new Maand("November");
        public static readonly Maand December = new Maand("December");

        public static IEnumerable<Maand> Maanden {
            get {
                yield return Januari;
                yield return Februari;
                yield return Maart;
                yield return April;
                yield return Mei;
                yield return Juni;
                yield return Juli;
                yield return Augustus;
                yield return September;
                yield return Oktober;
                yield return November;
                yield return December;

            }
        } 

        public static Maand GeefMaand(int nummer) {
            var maanden = Maanden;
            return maanden.ElementAt(nummer);
        }

        private Maand(String naam)
        {
            _naam = naam;
        }

        public String GeefNaam()
        {
            return _naam;
        }

    }

}
