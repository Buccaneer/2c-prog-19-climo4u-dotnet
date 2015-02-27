using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen
{
    public class ConstanteParameter: Parameter
    {
        private double _waarde;
    
        public override IComparable BerekenenWaarde(Models.Domein.Klimatogram klimatogram)
        {
            throw new NotImplementedException();
        }


        public override string GeefBeschrijving()
        {
            throw new NotImplementedException();
        }
    }
}
