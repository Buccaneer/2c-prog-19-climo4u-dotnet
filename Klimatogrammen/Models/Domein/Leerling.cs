using System;

namespace Klimatogrammen.Models.Domein
{

    public class Leerling
    {
        #region Fields
        /// <summary>
        /// Dit is een field om het jaar bij te houden
        /// </summary>
        private int? jaar;
        #endregion
        #region Properties
        /// <summary>
        /// Dit is een property dat de graad van de leerling bijhoudt aan de hand van een enum-klasse
        /// </summary>
        public Graad Graad { get; set; }

        /// <summary>
        /// Dit is een property dat het jaar van de leerling bijhoudt, als de leerling in graad twee zit. 
        /// Anders wordt er een ArgumentException gegooid.
        /// </summary>
        public int? Jaar
        {
            get { return jaar; }
            set
            {
                if (Graad.Equals(Graad.Een) || Graad.Equals(Graad.Drie))
                {
                    throw new ArgumentException("Het jaar kan enkel gekozen worden bij graad twee");
                }
                else
                {
                    jaar = value;
                }
            }

        }
        #endregion







    }
}
