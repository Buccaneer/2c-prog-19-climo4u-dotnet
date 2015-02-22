using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen
{
    public sealed class Maand
    {
        private readonly String _naam;

        public static readonly Maand JANUARI = new Maand("Januari");
        public static readonly Maand FEBRUARI = new Maand("Februari");
        public static readonly Maand MAART = new Maand("Maart");
        public static readonly Maand APRIL = new Maand("April");
        public static readonly Maand MEI = new Maand("Mei");
        public static readonly Maand JUNI = new Maand("Juni");
        public static readonly Maand JULI = new Maand("Juli");
        public static readonly Maand AUGUSTUS = new Maand("Augustus");
        public static readonly Maand SEPTEMBER = new Maand("September");
        public static readonly Maand OKTOBER = new Maand("Oktober");
        public static readonly Maand NOVEMBER = new Maand("November");
        public static readonly Maand DECEMBER = new Maand("December");

        private Maand(String naam)
        {
            _naam = naam;
        }

        public String geefNaam()
        {
            return _naam;
        }

    }

}
