using System;
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
        private double _waarde;

        public ConstanteParameter() {
            
        }

        public ConstanteParameter(double waarde)
        {
            _waarde = waarde;
        }
    
        public override IComparable BerekenWaarde(Klimatogram klimatogram)
        {
            return _waarde;
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
