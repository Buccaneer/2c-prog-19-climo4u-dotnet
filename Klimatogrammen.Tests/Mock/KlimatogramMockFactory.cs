using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klimatogrammen.Models.Domein;
using Moq;

namespace Klimatogrammen.Tests.Mock
{
    class KlimatogramMockFactory
    {

        public Mock<Klimatogram> MaakKlimatogramMock()
        {
            //Data
            int[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 18 };
            double[] gemiddeldeTemp = { 5.1, 2.0, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            string[] maandNamen =
            {
                "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September",
                "Oktober", "November", "December"
            };
            ICollection<Maand> maanden = new List<Maand>();
            //Mock
            var mock = new Mock<Klimatogram>();
            for (int i = 0; i < 12; i++)
            {
                maanden.Add(new Maand(mock.Object, maandNamen[i], gemiddeldeTemp[i], gemiddeldeNeer[i]));
            }
            mock.Setup(k => k.BeginJaar).Returns(1980);
            mock.Setup(k => k.EindJaar).Returns(2010);
            mock.Setup(k => k.Maanden).Returns(maanden);
            mock.Setup(k => k.Locatie).Returns("Mock Klimatogram");
            mock.Setup(k => k.GeefGemiddeldeTemperatuur()).Returns(gemiddeldeTemp.Average());
            mock.Setup(k => k.GeefTotaleNeerslag()).Returns(gemiddeldeNeer.Sum());
            mock.Setup(k => k.GeefTemperaturen()).Returns(gemiddeldeTemp);
            mock.Setup(k => k.GeefNeerslagen()).Returns(gemiddeldeNeer);
            mock.Setup(k => k.Latitude).Returns(3.7333);
            mock.Setup(k => k.Latitude).Returns(51.0003);
            return mock;
        }

    }
}
