namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Stelt een vergelijking voor tussen twee parameters. Een vergelijking in de vorm van A vergelijkingsoperator B.
    /// </summary>
    public class Vergelijking {
        public Operator Operator { get; set; }

        public Parameter LinkerParameter { get; set; }

        public Parameter RechterParameter { get; set; }

        /// <summary>
        /// Berekend het resultaat van deze vergelijking.
        /// </summary>
        /// <param name="klimatogram">Het klimatogram waarmee dat de parameters moeten worden opgevuld.</param>
        /// <returns>Resultaat vergelijking.</returns>
        public bool BerekenResultaat(Klimatogram klimatogram) {
            double links = (double)LinkerParameter.BerekenenWaarde(klimatogram);
            double rechts = (double)RechterParameter.BerekenenWaarde(klimatogram);

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
