using System;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Stelt een vergelijking voor tussen twee parameters. Een vergelijking in de vorm van A vergelijkingsoperator B.
    /// </summary>
    public class Vergelijking {
        public int VergelijkingId { get; set; }

        public Operator Operator { get; set; }

        public Parameter LinkerParameter { get; set; }

        public Parameter RechterParameter { get; set; }

        /// <summary>
        /// Berekent het resultaat van deze vergelijking.
        /// </summary>
        /// <param name="klimatogram">Het klimatogram waarmee dat de parameters moeten worden opgevuld.</param>
        /// <returns>Resultaat vergelijking.</returns>
        public bool BerekenResultaat(Klimatogram klimatogram) {


            double links = Double.Parse(LinkerParameter.BerekenWaarde(klimatogram).ToString());
            double rechts = Double.Parse(RechterParameter.BerekenWaarde(klimatogram).ToString());

            switch (Operator) {
                case Operator.GelijkAan:
                    return links.Equals(rechts);
                case Operator.NietGelijkAan:
                    return !links.Equals(rechts);
                case Operator.KleinerDan:
                    return links < rechts;
                case Operator.GroterDan:
                    return links > rechts;
                case Operator.KleinerDanOfGelijkAan:
                    return links <= rechts;
                default:
                    return links >= rechts;
            }
        }

        public Vergelijking() {
        }

        // @operator --> een '@' voor iets plaatsen maakt het mogelijk in C# om keywords toch als naam te gebruiken.
        public Vergelijking(Parameter linkerParameter, Operator @operator, Parameter rechterParameter) {
            LinkerParameter = linkerParameter;
            Operator = @operator;
            RechterParameter = rechterParameter;
        }
    }
}
