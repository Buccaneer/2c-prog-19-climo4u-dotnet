using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen
{
    public class BeslissingsKnoop : DeterminatieKnoop
    {
        public Vergelijking Vergelijking
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DeterminatieKnoop JaKnoop
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DeterminatieKnoop NeeKnoop
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DeterminatieKnoop NeemNeeKnoop(Klimatogram klimatogram)
        {
            throw new System.NotImplementedException();
        }

        public DeterminatieKnoop NeemJuisteKnoop(Klimatogram klimatogram)
        {
            throw new System.NotImplementedException();
        }

        public DeterminatieKnoop NeemJaKnoop(Klimatogram klimatogram)
        {
            throw new System.NotImplementedException();
        }
    }
}
