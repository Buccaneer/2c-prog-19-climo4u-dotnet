namespace Klimatogrammen.Models.Domein
{
    public class Vraag
    {

        public int VraagId { get; set; }

        public Resultaat Resultaat { get; set; }

        public Parameter Parameter { get; set; }

        public string VraagTekst { get; set; }

        public void ValideerVraag(string antwoord, Klimatogram klimatogram) {
            string juisteAntwoord = Parameter.BerekenWaarde(klimatogram).ToString();
            if (juisteAntwoord.Contains("|")) {
                Resultaat = juisteAntwoord.Contains(antwoord)
               ? Resultaat = Resultaat.Juist
               : Resultaat = Resultaat.Fout;
            } else
            Resultaat = juisteAntwoord.Equals(antwoord)
                ? Resultaat = Resultaat.Juist
                : Resultaat = Resultaat.Fout;
        }

        public Vraag()
        {
            Resultaat = Resultaat.Onbepaald;
        }
    }
}
