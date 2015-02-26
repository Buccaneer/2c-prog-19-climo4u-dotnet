using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen
{
    public class ParameterFactory
    {
        public Parameter MaakParameter(string code)
        {
            throw new System.NotImplementedException();
        }

        public ContansteParameter<double> MaakConstanteParameter(double waarde)
        {
            throw new System.NotImplementedException();
        }

        public ConstanteParameter<int> MaakConstanteParameter(int waarde)
        {
            throw new System.NotImplementedException();
        }
    }
}
