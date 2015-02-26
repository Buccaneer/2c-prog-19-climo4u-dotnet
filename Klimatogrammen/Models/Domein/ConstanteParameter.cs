using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen
{
    public class ConstanteParameter <T>: Parameter
    {
        private T _waarde;
    
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
