using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Klimatogrammen.Models.Domein
{

    public class Leerling
    {
        private ICollection<Klimatogram> _klimatogrammen;
        private ICollection<Klimatogram> _foutieveKlimatogrammen;
        #region Properties
        /// <summary>
        /// Dit is een property die het gekozen klimatogram van de leerling bijhoudt
        /// </summary>
        public Klimatogram Klimatogram { get; set; }

        public virtual Graad Graad { get; set; }
        #endregion


        public Continent GeefContinent(string continent)
        {
            return Graad.GeefContinent(continent);
        }

        public ICollection<Continent> GeefContinenten()
        {
            return Graad.GeefContinenten();
        }

        public string[] ValideerVragen(string[] antwoord)
        {
            return Graad.ValideerVragen(antwoord, Klimatogram);
        }

        public ICollection<Vraag> GeefVragen()
        {
            return Graad.GeefVragen();
        }

        /// <summary>
        /// Geeft indien mogelijk 6 random klimatogrammen met allemaal verschillende vegetatietypes terug.
        /// </summary>
        /// <returns></returns>
        /// <remarks>In een scenario zal dit niet werken. Loopt niet oneindig lang maar stopt.</remarks>
        public virtual ICollection<Klimatogram> GeefKlimatogrammenDerdeGraad()
        {
            if (_klimatogrammen != null)
                return _klimatogrammen;

            const int maximumLoop = 10; // Maximum tien keer de databank belasten, daarna stop. (tegen oneindige lus.)
            int poging = 0;
            var klimatogrammen = new List<Klimatogram>();
            var vegTypes = new List<string>();
            Random r = new Random(DateTime.Now.Millisecond);

            while (poging < maximumLoop || klimatogrammen.Count == 6)
            {
                poging++;
                var zesWillekeurigeKlimatogrammen = new List<Klimatogram>();
                for (int i = 0; i < 6; ++i)
                {
                    int aantalC = Graad.Continenten.Count;
                    var continent = Graad.Continenten.ElementAt(r.Next(aantalC));
                    int aantalL = continent.Landen.Count;
                    var land = continent.Landen.ElementAt(r.Next(aantalL));
                    zesWillekeurigeKlimatogrammen.Add(land.Klimatogrammen.ElementAt(r.Next(land.Klimatogrammen.Count)));
                }

                foreach (var klimatogram in zesWillekeurigeKlimatogrammen)
                {
                    var resultaat = Graad.DeterminatieTabel.Determineer(klimatogram).VegetatieType.Naam;
                    if (!vegTypes.Contains(resultaat))
                    {
                        vegTypes.Add(resultaat);
                        klimatogrammen.Add(klimatogram);
                    }
                    if (klimatogrammen.Count == 6)
                    {
                        _foutieveKlimatogrammen = klimatogrammen;
                        return _klimatogrammen = klimatogrammen;
                    }
                }
            }
            _foutieveKlimatogrammen = klimatogrammen;
            return _klimatogrammen = klimatogrammen;
        }

        public virtual ICollection<Klimatogram> FoutieveKlimatogrammenDerdeJaar
        {
            get { return _foutieveKlimatogrammen; }
        }

        public void ValideerLocaties(string[] locaties, string[] klimatogrammen)
        {
            if (FoutieveKlimatogrammenDerdeJaar.Count == 0 || locaties.Length != FoutieveKlimatogrammenDerdeJaar.Count)
                return;
            var klims = new List<Klimatogram>();
            for (int i = 0; i < locaties.Length; ++i)
            {
                int locatie = int.Parse(locaties[i]);
                Klimatogram klimatogram = _klimatogrammen.First(k => k.Locatie.Equals(klimatogrammen[i]));
                if (FoutieveKlimatogrammenDerdeJaar.ElementAt(locatie) != klimatogram)
                    klims.Add(klimatogram);
            }
            _foutieveKlimatogrammen = klims;
        }
    }
}

