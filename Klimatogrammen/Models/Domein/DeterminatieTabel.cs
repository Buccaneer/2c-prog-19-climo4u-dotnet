using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen
{
    public class DeterminatieTabel
    {
        public DeterminatieKnoop BeginKnoop
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public LinkedList<DeterminatieKnoop> GebruikersPad
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public LinkedList<DeterminatieKnoop> JuistePad
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public ResultaatKnoop Determineer(Klimatogram klimatogram)
        {
            throw new System.NotImplementedException();
        }

        public void NeemJaKnoop(Klimatogram klimatogram)
        {
            throw new System.NotImplementedException();
        }

        public void NeemNeeKnoop(Klimatogram klimatogram)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Valideert het door gebruiker gekozen pad en wanneer er een fout in zit splits het gebruikerspad op en onthoud enkel het juiste stuk.
        /// </summary>
        public Resultaat ValideerGebruikersPad()
        {
            throw new System.NotImplementedException();
        }

        public bool HeeftLeerlingEindeBereikt()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Enkel als de gebruiker bij een leaf node zit kan dit een resultaat terug geven. Anders Exception.
        /// </summary>
        public Klimatogrammen.DeterminatieKnoop GeefDeterminatieGebruiker()
        {
            throw new System.NotImplementedException();
        }
    }
}
