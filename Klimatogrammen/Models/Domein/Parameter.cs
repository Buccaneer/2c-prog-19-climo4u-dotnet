﻿using System;
using System.Collections.Generic;

namespace Klimatogrammen.Models.Domein
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
