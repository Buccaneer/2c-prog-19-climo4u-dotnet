using System;

namespace Klimatogrammen.Models.Domein
{
    public class ParameterFactory
    {
        public Parameter MaakParameter(string code)
        {
            switch (code)
            {
                case "Warmste Maand":
                    return new ParameterWarmsteMaand() {ParameterId = "Warmste Maand"};
                case "Koudste Maand":
                    return new ParameterKoudsteMaand() {ParameterId = "Koudste Maand"};
                case "Tw":
                    return new ParameterTemperatuurWarmsteMaand() {ParameterId = "Tw"};
                case "Tk":
                    return new ParameterTemperatuurKoudsteMaand() {ParameterId = "Tk"};
                case "Nz":
                    return new ParameterNeerslagZomer() {ParameterId = "Nz"};
                case "Nw":
                    return new ParameterNeerslagWinter() {ParameterId = "Nw"};
                case "D":
                    return new ParameterAantalDrogeMaanden() {ParameterId = "D"};
                case "Tj":
                    return new ParameterGemiddeldeTemperatuurJaar() {ParameterId = "Tj"};
                case "Nj":
                    return new ParameterTotaleNeerslagJaar() {ParameterId = "Nj"};
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
