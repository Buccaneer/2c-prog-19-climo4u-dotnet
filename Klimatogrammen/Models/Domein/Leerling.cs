﻿using System;

namespace Klimatogrammen.Models.Domein
{

    public class Leerling
    {
        #region Fields
        /// <summary>
        /// Dit is een Januari om het jaar bij te houden
        /// </summary>
        private int? jaar;
        #endregion
        #region Properties


        /// <summary>
        /// Dit is een property dat het jaar van de leerling bijhoudt, als de leerling in graad twee zit. 
        /// Anders wordt er een ArgumentException gegooid.
        /// </summary>
        public int? Jaar
        {
            get { return jaar; }
            set
            {
                if (Graad == Graad.Een || Graad == Graad.Drie) {
                    if (value == null)
                        return;
                    throw new ArgumentException("Het jaar kan enkel gekozen worden bij graad twee.");
                }

                if (value == null)
                    throw new ArgumentException("Het jaar kan niet null zijn.");
                if (value < 1 || value > 2)
                    throw new ArgumentException("Het jaar moet tussen 1 en 2 liggen.");

                jaar = value;
            }

        }
        #endregion

        /// <summary>
        /// Dit is een property die het gekozen klimatogram van de leerling bijhoudt
        /// </summary>
        public Klimatogram Klimatogram { get; set; }

        public Graad Graad
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }








    }
}
