using System;

namespace Klimatogrammen.Models.Domein
{
    public class ParameterFactory
    {
        private Parameter[] _parameters = new Parameter[9];
        public Parameter MaakParameter(string code)
        {
            switch (code)
            {
                case "Warmste Maand":
                    return _parameters[0] ??
                           (_parameters[0] = new ParameterWarmsteMaand() {ParameterId = "Warmste Maand"});
                case "Koudste Maand":
                    return _parameters[1] ??
                           (_parameters[1] = new ParameterKoudsteMaand() {ParameterId = "Koudste Maand"});
                case "Tw":
                    return _parameters[2] ??
                           (_parameters[2] =new ParameterTemperatuurWarmsteMaand() {ParameterId = "Tw"});
                case "Tk":
                    return _parameters[3] ??
                           (_parameters[3] =new ParameterTemperatuurKoudsteMaand() {ParameterId = "Tk"});
                case "Nz":
                    return _parameters[4] ??
                           (_parameters[4] =new ParameterNeerslagZomer() {ParameterId = "Nz"});
                case "Nw":
                    return _parameters[5] ??
                           (_parameters[5] =new ParameterNeerslagWinter() {ParameterId = "Nw"});
                case "D":
                    return _parameters[6] ??
                           (_parameters[6] = new ParameterAantalDrogeMaanden() {ParameterId = "D"});
                case "Tj":
                    return _parameters[7] ??
                           (_parameters[7] =new ParameterGemiddeldeTemperatuurJaar() {ParameterId = "Tj"});
                case "Nj":
                    return _parameters[8] ??
                           (_parameters[8] =new ParameterTotaleNeerslagJaar() {ParameterId = "Nj"});
                default:
                    throw new ArgumentException("Er werd een ongeldige code meegegeven");
            }
        }

        public Parameter MaakConstanteParameter(double waarde)
        {
            return new ConstanteParameter(waarde) {ParameterId = Guid.NewGuid().ToString()};
        }

        public Parameter MaakConstanteParameter(int waarde)
        {
            return new ConstanteParameter(waarde) {ParameterId = Guid.NewGuid().ToString()};
        }

    }
}
