using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public abstract class Parameter
    {
        /// <summary>
        /// Bijvoorbeeld: TK, TW
        /// </summary>
        public string ParameterId { get; set; }

        public abstract IComparable BerekenWaarde(Klimatogram klimatogram);

        public abstract string GeefBeschrijving();

        public abstract ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram);

    }
}
