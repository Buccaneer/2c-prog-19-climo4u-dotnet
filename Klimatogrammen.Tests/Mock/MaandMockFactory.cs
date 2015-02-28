using Klimatogrammen.Models.Domein;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Klimatogrammen.Tests.Mock
{
    class MaandMockFactory
    {

        public Mock<Maand> MaakMaandMock(Klimatogram k, String naam, int neerslag, double temperatuur)
        {
            var mock = new Mock<Maand>();
            mock.Setup(m => m.Klimatogram).Returns(k);
            mock.Setup(m => m.Naam).Returns(naam);
            mock.Setup(m => m.Neerslag).Returns(neerslag);
            mock.Setup(m => m.Temperatuur).Returns(temperatuur);
            return mock;
        }

        public Mock<Maand> MaakMaandMock(String naam, int neerslag, double temperatuur)
        {
            return MaakMaandMock(null, naam, neerslag, temperatuur);
        }

        public ICollection<Mock<Maand>> MaakMaandenInJaarMock(Klimatogram k, int[] neerslagen, double[] temperaturen)
        {
            string[] maandNamen =
            {
                "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September",
                "Oktober", "November", "December"
            };
            var maanden = new List<Mock<Maand>>();
            for (int i = 0; i < 12; i++)
            {
                maanden.Add(MaakMaandMock(k, maandNamen[i], neerslagen[i], temperaturen[i]));
            }
            return maanden;
        }

        public ICollection<Mock<Maand>> MaakMaandenInJaarMock(int[] neerslagen, double[] temperaturen)
        {
            return MaakMaandenInJaarMock(null, neerslagen, temperaturen);
        }

    }
}
