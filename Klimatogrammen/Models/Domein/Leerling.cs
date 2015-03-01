using System;

namespace Klimatogrammen.Models.Domein
{

    public class Leerling
    {

        #region Properties
        /// <summary>
        /// Dit is een property die het gekozen klimatogram van de leerling bijhoudt
        /// </summary>
        public Klimatogram Klimatogram { get; set; }

        public Graad Graad { get; set; }
        #endregion

    }
}
