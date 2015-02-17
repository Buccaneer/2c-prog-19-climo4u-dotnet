using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Microsoft.Ajax.Utilities;

namespace Klimatogrammen.Models.Domein {
    /// <summary>
    /// Wrapper klasse voor temperatuur te kunnen laten mappen door EF.
    /// </summary>
    public class Temperatuur : IEquatable<Temperatuur> {
        private double _waarde;
        public Temperatuur() {
          
        }

        public Temperatuur(double waarde) {
            Waarde = waarde;
        }

        /// <summary>
        /// De effectieve waarde.
        /// </summary>
        public double Waarde {
            get { return _waarde; }
            set {
                if (value < -273.15 || value > 500) {
                    throw new ArgumentOutOfRangeException("De waarde van een gemiddelde temperatuur moet tussen -273.15 en 500 liggen.");
                }
                _waarde = value;
            }
        }

        public int TemperatuurId { get; set; }

        public bool Equals(Temperatuur other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _waarde.Equals(other._waarde);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Temperatuur) obj);
        }

        public override int GetHashCode() {
            return _waarde.GetHashCode();
        }

        #region "impliciet casten"
        /// <summary>
        /// Impliciet casten van een double naar een Temperatuur object.
        /// Bij impliciet casten bepaalt de runtime naar welk type dit moet gecast worden.
        /// Dus je kan als doende casten:
        /// Temperatuur t = 65.0;
        /// Je hoeft niet
        /// Temperatuur t = (Temperatuur)65.0;
        /// Casten wordt in dotnet als een operator aanzien. Je kan alle operators overloaden (overschrijven) in DotNet.
        ///
        /// </summary>
        /// <param name="waarde">De waarde die de temperatuur is.</param>
        /// <returns>Een temperatuur object.</returns>
        public static implicit operator Temperatuur(double waarde) {
            return new Temperatuur(waarde);
        }

        /// <summary>
        /// Impliciet casten van Temperatuur naar double.
        /// </summary>
        /// <param name="temperatuur">De gegeven temperatuur</param>
        /// <returns>De waarde</returns>
        public static implicit operator double(Temperatuur temperatuur) {
            return temperatuur.Waarde;
        }
        #endregion

        #region "overschreven wiskundige operatoren"
        /// <summary>
        /// Maakt het mogelijk om twee temperaturen bij elkaar op te tellen.
        /// </summary>
        /// <param name="links">Het linkerlid</param>
        /// <param name="rechts">Het rechterlid</param>
        /// <returns>Een nieuwe temperatuur met de som van rechts en links.</returns>
        /// <remarks>Meer informatie over Operator Overloading:
        /// https://msdn.microsoft.com/en-us/library/aa288467%28v=vs.71%29.aspx </remarks>
        public static Temperatuur operator +(Temperatuur links, Temperatuur rechts) {
            return new Temperatuur(links.Waarde + rechts.Waarde);
        }

        public static Temperatuur operator -(Temperatuur links, Temperatuur rechts) {
            return new Temperatuur(links.Waarde - rechts.Waarde);
        }

        public static Temperatuur operator *(Temperatuur links, Temperatuur rechts) {
            return new Temperatuur(links.Waarde * rechts.Waarde);
        }

        public static Temperatuur operator /(Temperatuur links, Temperatuur rechts) {
            return new Temperatuur(links.Waarde / rechts.Waarde);
        }

        public static Temperatuur operator -(Temperatuur links) {
            return new Temperatuur(-links.Waarde);
        }
        #endregion

        #region "overschreven logische operatoren"
        public static bool operator ==(Temperatuur links, Temperatuur rechts) {
            return (links != null && rechts != null) && (links.Waarde.Equals(rechts.Waarde));
        }

        public static bool operator !=(Temperatuur links, Temperatuur rechts) {
            return (links != null && rechts != null) && (!links.Waarde.Equals(rechts.Waarde));
        }

        public static bool operator <(Temperatuur links, Temperatuur rechts) {
            return (links.Waarde < rechts.Waarde);
        }

        public static bool operator >(Temperatuur links, Temperatuur rechts) {
            return (links.Waarde > rechts.Waarde);
        }

        public static bool operator <=(Temperatuur links, Temperatuur rechts) {
            return (links.Waarde <= rechts.Waarde);
        }

        public static bool operator >=(Temperatuur links, Temperatuur rechts) {
            return (links.Waarde >= rechts.Waarde);
        }
        #endregion
    }
}
