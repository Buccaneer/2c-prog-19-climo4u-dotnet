using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public abstract class Parameter
    {
        /// <summary>
        /// Bijvoorbeeld: TK, TW
        /// </summary>
        public string ParameterId
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public abstract IComparable BerekenenWaarde(Klimatogram klimatogram);

        public abstract string GeefBeschrijving();
    }
}
