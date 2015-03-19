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
            //Werelddeel c = new Werelddeel("Europa");
            //Land belgie = new Land("België");


            //int[] gemiddeldeNeerUkkel = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
            //double[] gemiddeldeTempUkkel = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            //belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(gemiddeldeTempUkkel, gemiddeldeNeerUkkel).ToList(), 50.3, 4.5) { Locatie = "Ukkel", BeginJaar = 1961, EindJaar = 2009 });
            //c.VoegLandToe(belgie);

            //mock.Setup(g => g.Werelddelen).Returns(new Werelddeel[] { c });
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
        //    Werelddeel c = new Werelddeel("Europa");
        //    Land belgie = new Land("België");


        //    int[] gemiddeldeNeerUkkel = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
        //    double[] gemiddeldeTempUkkel = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
        //    belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(gemiddeldeTempUkkel, gemiddeldeNeerUkkel).ToList(), 1.5, 1.5) { Locatie = "Ukkel", BeginJaar = 1961, EindJaar = 2009 });
        //    c.VoegLandToe(belgie);

        //    Land kameroen = new Land("Kameroen");
        //    kameroen.VoegKlimatogramToe(new Klimatogram(VormMaanden(
        //    new double[] { 23.7, 25.3, 25.0, 24.6, 24.1, 23.4, 22.6, 23.0, 23.1, 23.3, 23.7, 23.7 },
        //    new int[] { 17, 51, 140, 180, 220, 162, 70, 102, 254, 296, 111, 25 }).ToList(), 40.5, -6.3
        //) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Yaounde" });

        //    Werelddeel afrika = new Werelddeel("afrika");
        //    afrika.Landen.Add(kameroen);
        //    mock.Setup(g => g.Werelddelen).Returns(new Werelddeel[] { c, afrika });
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
