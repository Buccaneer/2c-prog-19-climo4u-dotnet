﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public class ConstanteParameter: Parameter
    {
        public double Waarde { get; set; }

        public ConstanteParameter() {
            
        }

        public ConstanteParameter(double waarde) {
            Waarde = waarde;
        }
    
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return Waarde;
        }


        public override string GeefBeschrijving()
        {
            throw new NotSupportedException();
        }

        public override ICollection<string> GeefMogelijkeAntwoorden(Klimatogram klimatogram)
        {
            throw new NotSupportedException();
        }
    }
}
