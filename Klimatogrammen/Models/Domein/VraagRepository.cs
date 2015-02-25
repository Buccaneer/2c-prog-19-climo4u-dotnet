using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Klimatogrammen.ViewModels;
using WebGrease.Css.Extensions;

namespace Klimatogrammen.Models.Domein
{
    public class VraagRepository
    {
        private ICollection<Vraag> _juisteVragen;
        private ICollection<Vraag> _foutieveVragen;

        public ICollection<Vraag> Vragen { get; set; }

        public ICollection<Vraag> JuisteVragen
        {
            get { return _juisteVragen; }
        }

        public ICollection<Vraag> FoutieveVragen
        {
            get { return _foutieveVragen; }
        }

        public VraagRepository()
        {
            Vragen = new Collection<Vraag>();
            _juisteVragen=new Collection<Vraag>();
            _foutieveVragen=new Collection<Vraag>();
        }

        public void ValideerVraag(int vraag, string antwoord)
        {
            Vraag v = Vragen.ElementAt(vraag);
            v.ValideerVraag(antwoord);
            if (v.Resultaat.Equals(Resultaat.Juist))
            {
                _juisteVragen.Add(v);
            }
            else
            {
                _foutieveVragen.Add(v);
            }
        }

        public static VraagRepository CreerVragenVoorKlimatogram(Klimatogram klimatogram)
        {
            VraagRepository repo = new VraagRepository();
            repo.Vragen.Add(new VraagWarmsteMaand(klimatogram));
            repo.Vragen.Add(new VraagTemperatuurWarmsteMaand(klimatogram));
            repo.Vragen.Add(new VraagKoudsteMaand(klimatogram));
            repo.Vragen.Add(new VraagTemperatuurKoudsteMaand(klimatogram));
            repo.Vragen.Add(new VraagAantalDrogeMaanden(klimatogram));
            repo.Vragen.Add(new VraagNeerslagInDeZomer(klimatogram));
            repo.Vragen.Add(new VraagNeerslagInDeWinter(klimatogram));
            return repo;
        }
    }
}
