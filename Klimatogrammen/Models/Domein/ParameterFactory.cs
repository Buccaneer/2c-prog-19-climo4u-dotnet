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
            switch (code)
            {
                case "Warmste Maand":
                    return new ParameterWarmsteMaand();
                case "Koudste Maand":
                    return new ParameterKoudsteMaand();
                case "Tw":
                    return new ParameterTemperatuurWarmsteMaand();
                case "Tk":
                    return new ParameterTemperatuurKoudsteMaand();
                case "Nz":
                    return new ParameterNeerslagZomer();
                case "Nw":
                    return new ParameterNeerslagWinter();
                case "D":
                    return new ParameterAantalDrogeMaanden();
                case "Tj":
                    return new ParameterGemiddeldeTemperatuurJaar();
                case "Nj":
                    return new ParameterTotaleNeerslagJaar();
                default:
                    throw new ArgumentException("Er werd een ongeldige code meegegeven");
            }
        }

        public Parameter MaakConstanteParameter(double waarde)
        {
            return new ConstanteParameter(waarde);
        }

        public Parameter MaakConstanteParameter(int waarde)
        {
            return new ConstanteParameter(waarde);
        }

    }
}
