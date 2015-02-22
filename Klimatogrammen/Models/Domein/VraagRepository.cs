using System.Collections.Generic;

namespace Klimatogrammen.Models.Domein
{
    public class VraagRepository
    {
        public ICollection<Vraag> Vragen
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public ICollection<Vraag> JuisteVragen
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public ICollection<Vraag> FoutieveVragen
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void ValideerVraag(int vraag, string antwoord)
        {
            throw new System.NotImplementedException();
        }

        public static VraagRepository CreerVragenVoorKlimatogram(Klimatogram klimatogram)
        {
            throw new System.NotImplementedException();
        }
    }
}
