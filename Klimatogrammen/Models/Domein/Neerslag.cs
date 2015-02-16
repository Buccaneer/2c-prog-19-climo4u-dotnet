using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Wrapper klasse voor Neerslag te kunnen laten mappen door EF.
    /// </summary>
    public class Neerslag : IEquatable<Neerslag> {
        private int _waarde;
        public Neerslag() {
            
        }

        public Neerslag(int waarde) {
            Waarde = waarde;
        }

        public bool Equals(Neerslag other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _waarde == other._waarde;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Neerslag) obj);
        }

        public override int GetHashCode() {
            return _waarde;
        }

        /// <summary>
    /// De effectieve waarde van neerslag.
    /// </summary>
        public int Waarde {
            get { return _waarde; }
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException("De waarde van de neerslag per maand mag niet negatief zijn.");
                }
                _waarde = value;
            }
        }

        #region "impliciet casten"
        /// <summary>
        /// Impliciet casten van een int naar een Neerslag object.
        /// Bij impliciet casten bepaalt de runtime naar welk type dit moet gecast worden.
        /// Dus je kan als doende casten:
        /// Neerslag t = 65;
        /// Je hoeft niet
        /// Neerslag t = (Neerslag)65;
        /// Casten wordt in dotnet als een operator aanzien. Je kan alle operators overloaden (overschrijven) in DotNet.
        ///
        /// </summary>
        /// <param name="waarde">De waarde die de Neerslag is.</param>
        /// <returns>Een Neerslag object.</returns>
        public static implicit operator Neerslag(int waarde) {
            return new Neerslag(waarde);
        }

        /// <summary>
        /// Impliciet casten van Neerslag naar int.
        /// </summary>
        /// <param name="Neerslag">De gegeven Neerslag</param>
        /// <returns>De waarde</returns>
        public static implicit operator int(Neerslag Neerslag) {
            return Neerslag.Waarde;
        }
        #endregion

        #region "overschreven wiskundige operatoren"
        /// <summary>
        /// Maakt het mogelijk om twee neerslagen bij elkaar op te tellen.
        /// </summary>
        /// <param name="links">Het linkerlid</param>
        /// <param name="rechts">Het rechterlid</param>
        /// <returns>Een nieuwe Neerslag met de som van rechts en links.</returns>
        /// <remarks>Meer informatie over Operator Overloading:
        /// https://msdn.microsoft.com/en-us/library/aa288467%28v=vs.71%29.aspx </remarks>
        public static Neerslag operator +(Neerslag links, Neerslag rechts) {
            return new Neerslag(links.Waarde + rechts.Waarde);
        }

        public static Neerslag operator -(Neerslag links, Neerslag rechts) {
            return new Neerslag(links.Waarde - rechts.Waarde);
        }

        public static Neerslag operator *(Neerslag links, Neerslag rechts) {
            return new Neerslag(links.Waarde * rechts.Waarde);
        }

        public static Neerslag operator /(Neerslag links, Neerslag rechts) {
            return new Neerslag(links.Waarde / rechts.Waarde);
        }

        public static Neerslag operator -(Neerslag links) {
            return new Neerslag(-links.Waarde);
        }
        #endregion

        #region "overschreven logische operatoren"
        public static bool operator ==(Neerslag links, Neerslag rechts) {
            return (links != null && rechts != null) && (links.Waarde.Equals(rechts.Waarde));
        }

        public static bool operator !=(Neerslag links, Neerslag rechts) {
            return (links != null && rechts != null) && (!links.Waarde.Equals(rechts.Waarde));
        }

        public static bool operator <(Neerslag links, Neerslag rechts) {
            return (links.Waarde < rechts.Waarde);
        }

        public static bool operator >(Neerslag links, Neerslag rechts) {
            return (links.Waarde > rechts.Waarde);
        }

        public static bool operator <=(Neerslag links, Neerslag rechts) {
            return (links.Waarde <= rechts.Waarde);
        }

        public static bool operator >=(Neerslag links, Neerslag rechts) {
            return (links.Waarde >= rechts.Waarde);
        }
        #endregion
    }
}
