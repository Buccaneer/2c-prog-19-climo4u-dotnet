namespace Klimatogrammen.Models.Domein
{
    public class Vraag
    {

        public int VraagId { get; set; }

        public Resultaat Resultaat { get; set; }

        public Parameter Parameter { get; set; }

        public string VraagTekst { get; set; }

        public void ValideerVraag(string antwoord, Klimatogram klimatogram)
        {
            Resultaat = Parameter.BerekenWaarde(klimatogram).ToString().Equals(antwoord)
                ? Resultaat = Resultaat.Juist
                : Resultaat = Resultaat.Fout;
        }

        public Vraag()
        {
            Resultaat = Resultaat.Fout;
        }
    }
}
