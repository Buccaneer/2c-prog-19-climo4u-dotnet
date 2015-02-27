using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public class Vraag
    {

        public Resultaat Resultaat { get; set; }

        public Parameter Parameter
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string VraagTekst
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        public void ValideerVraag(string antwoord, Klimatogram klimatogram) {
            throw new System.NotImplementedException();
}
    }
}
