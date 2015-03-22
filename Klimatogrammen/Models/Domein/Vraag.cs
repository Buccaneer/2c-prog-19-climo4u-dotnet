namespace Klimatogrammen.Models.Domein
{
    /// <summary>
    /// Vraag is een klasse dat het gebruikersantwoord bijhoudt en een vraag kan valideren
    /// </summary>
    public class Vraag
    {

        public int VraagId { get; set; }

        public Parameter Parameter { get; set; }

        public string GebruikersAntwoord { get; set; }

        public string VraagTekst { get; set; }

        public Resultaat ValideerVraag(string antwoord, Klimatogram klimatogram) {
            string juisteAntwoord = Parameter.BerekenWaarde(klimatogram).ToString();
            if (juisteAntwoord.Contains("|")) {
                return juisteAntwoord.Contains(antwoord)
               ? Resultaat.Juist
               : Resultaat.Fout;
            } else
            return juisteAntwoord.Equals(antwoord)
                ? Resultaat.Juist
                : Resultaat.Fout;
        }

        public Vraag()
        {
        }
    }
}
