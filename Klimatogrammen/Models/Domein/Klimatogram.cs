using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen {
    public class Klimatogram {
        public IDictionary<int, double[]> Temperaturen {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public IDictionary<int, int[]> Neerslagen {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public double[] GemiddeldeTemperatuur {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public int[] GemiddeldeNeerslag {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public string Locatie {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public int TotaleNeerslag {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public double TotaalGemiddeldeTemperatuur {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        private T[] BerekenGemiddelde<T>(IDictionary<int, T[]> gegevens) {
            throw new System.NotImplementedException();
        }
    }
}
