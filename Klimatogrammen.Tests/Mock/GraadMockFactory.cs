using System.Data.Entity.Spatial;
using Klimatogrammen.Models.Domein;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klimatogrammen.Tests.Mock
{
    class GraadMockFactory
    {

        public Mock<Graad> MaakEersteGraadAan()
        {
            int graad = 1;
            Mock<Graad> mock = new Mock<Graad>();

            mock.Setup(g => g.Nummer).Returns(graad);
            mock.Setup(g => g.Vragen)
                .Returns(new Vraag[] { new Vraag() { Parameter = new ParameterWarmsteMaand(), VraagTekst = "Geef warmste maand?" } });
            mock.Setup(g => g.Continenten).Returns(new ContinentFactory().MaakContinenten(graad));
            DeterminatieTabel determinatieTabel = new DeterminatieTabelMock().MaakDeterminatieTabelEersteGraad();
            determinatieTabel.Determineer(new KlimatogramMockFactory().MaakKlimatogramMock().Object);
            mock.Setup(l => l.DeterminatieTabel).Returns(determinatieTabel);

            return mock;
        }

        public Mock<Graad> MaakTweedeGraadEersteJaarAan()
        {
            int graad = 2;
            Mock<Graad> mock = new Mock<Graad>();

            mock.Setup(g => g.Nummer).Returns(graad);
            mock.Setup(g => g.Jaar).Returns(1);
            mock.Setup(g => g.Continenten).Returns(new ContinentFactory().MaakContinenten(graad));
            DeterminatieTabel determinatieTabel = new GroteDeterminatieTabelMock().MaakDeterminatieTabel();
            determinatieTabel.Determineer(new KlimatogramMockFactory().MaakKlimatogramMock().Object);
            mock.Setup(l => l.DeterminatieTabel).Returns(determinatieTabel);

            return mock;
        }

        public Mock<Graad> MaakTweedeGraadTweedeJaarAan()
        {
            int graad = 2;
            Mock<Graad> mock = new Mock<Graad>();

            mock.Setup(g => g.Nummer).Returns(graad);
            mock.Setup(g => g.Jaar).Returns(2);
            mock.Setup(g => g.Continenten).Returns(new ContinentFactory().MaakContinenten(graad));
            DeterminatieTabel determinatieTabel = new GroteDeterminatieTabelMock().MaakDeterminatieTabel();
            determinatieTabel.Determineer(new KlimatogramMockFactory().MaakKlimatogramMock().Object);
            mock.Setup(l => l.DeterminatieTabel).Returns(determinatieTabel);
            
            return mock;
        }

        public Mock<Graad> MaakDerdeGraadAan()
        {
            int graad = 3;
            Mock<Graad> mock = new Mock<Graad>();

            mock.Setup(g => g.Nummer).Returns(graad);
            mock.Setup(g => g.Continenten).Returns(new ContinentFactory().MaakContinenten(graad));
            mock.Setup(l => l.DeterminatieTabel).Returns(new GroteDeterminatieTabelMock().MaakDeterminatieTabel);
            
            return mock;
        }

        private IEnumerable<Maand> VormMaanden(double[] temperaturen, int[] neerslagen)
        {
            string[] maanden = new string[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };
            for (int i = 0; i < maanden.Length; ++i)
                yield return new Maand(maanden[i], temperaturen[i], neerslagen[i]);
        }

    }
}
