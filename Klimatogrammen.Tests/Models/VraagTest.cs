using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Klimatogrammen.Models.Domein;
using System.Collections.Generic;

namespace Klimatogrammen.Tests.Models
{
    [TestClass]
    public class VraagTest
    {

        Mock<Klimatogram> _mockKlimatogram;
        //List<Maand> alleMaanden;
        private ICollection _alleMaandenString;

        [TestInitialize]
        public void Init()
        {
            _mockKlimatogram = mockKlimatogramTrainen();
            //alleMaanden = maakLijstMetAlleMaanden();
            _alleMaandenString = maakLijstMetAlleMaandenString();
        }

        private Mock<Klimatogram> mockKlimatogramTrainen()
        {
            var mock = new Mock<Klimatogram>();
            mock.Setup(k => k.BeginJaar).Returns(1980);
            mock.Setup(k => k.EindJaar).Returns(2010);
            Neerslag[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 18 };
            Temperatuur[] gemiddeldeTemp = { 5.1, 2.0, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
            mock.Setup(k => k.GemiddeldeNeerslag).Returns(gemiddeldeNeer);
            mock.Setup(k => k.GemiddeldeTemperatuur).Returns(gemiddeldeTemp);
            mock.Setup(k => k.Locatie).Returns("Mock Klimatogram");
            mock.Setup(k => k.TotaalGemiddeldeTemperatuur).Returns(5.1 + 2.2 + 10.5 + 12.7 + 18 + 20.4 + 21.2 + 25.2 + 30.1 + 19 + 10 + 2.2 / 12);
            mock.Setup(k => k.TotaalNeerslag).Returns(174);
            return mock;
        }

        //private List<Maand> maakLijstMetAlleMaanden()
        //{
        //    List<Maand> m = new List<Maand>();
        //    m.Add(Maand.JANUARI);
        //    m.Add(Maand.FEBRUARI);
        //    m.Add(Maand.MAART);
        //    m.Add(Maand.APRIL);
        //    m.Add(Maand.MEI);
        //    m.Add(Maand.JUNI);
        //    m.Add(Maand.JULI);
        //    m.Add(Maand.AUGUSTUS);
        //    m.Add(Maand.SEPTEMBER);
        //    m.Add(Maand.OKTOBER);
        //    m.Add(Maand.NOVEMBER);
        //    m.Add(Maand.DECEMBER);
        //    return m;
        //}

        private ICollection maakLijstMetAlleMaandenString()
        {
            ICollection<string> m = new List<string>();
            m.Add(Maand.Januari.GeefNaam());
            m.Add(Maand.Februari.GeefNaam());
            m.Add(Maand.Maart.GeefNaam());
            m.Add(Maand.April.GeefNaam());
            m.Add(Maand.Mei.GeefNaam());
            m.Add(Maand.Juni.GeefNaam());
            m.Add(Maand.Juli.GeefNaam());
            m.Add(Maand.Augustus.GeefNaam());
            m.Add(Maand.September.GeefNaam());
            m.Add(Maand.Oktober.GeefNaam());
            m.Add(Maand.November.GeefNaam());
            m.Add(Maand.December.GeefNaam());
            return m.ToList();
        }

        #region VraagWarmsteMaandTesten

        [TestMethod]
        public void VraagWarmsteMaandGeeftMogelijkeAntwoorden()
        {
            Vraag v = new VraagWarmsteMaand(_mockKlimatogram.Object);
            ICollection<string> mogelijkeAntwoorden = v.GeefMogelijkeAntwoorden();
            CollectionAssert.AreEquivalent(_alleMaandenString, mogelijkeAntwoorden.ToList());
        }

        [TestMethod]
        public void VraagWarmsteMaandValideertVraagJuist()
        {
            Vraag v = new VraagWarmsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("September");
            Assert.AreEqual(Resultaat.Juist, v.Resultaat);
        }

        [TestMethod]
        public void VraagWarmsteMaandValideertVraagFout()
        {
            Vraag v = new VraagWarmsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("Oktober");
            Assert.AreEqual(Resultaat.Fout, v.Resultaat);
        }

        [TestMethod]
        public void VraagWarmsteMaandGeeftValidatieTekstBijJuist()
        {
            Vraag v = new VraagWarmsteMaand(_mockKlimatogram.Object);
            string antwoord = "September";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is correct.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagWarmsteMaandGeeftValidatieTekstBijFout()
        {
            Vraag v = new VraagWarmsteMaand(_mockKlimatogram.Object);
            string antwoord = "Oktober";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is fout.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagWarmsteMaandGeeftVraagTekst()
        {
            Vraag v = new VraagWarmsteMaand(_mockKlimatogram.Object);
            Assert.AreEqual("Welke is de warmste maand?", v.GeefVraagTekst());
        }

        #endregion

        #region VraagTemperatuurWarmsteMaandTesten

        [TestMethod]
        public void VraagTemperatuurWarmsteMaandGeeftMogelijkeAntwoorden()
        {
            Vraag v = new VraagTemperatuurWarmsteMaand(_mockKlimatogram.Object);
            ICollection<string> mogelijkeAntwoorden = v.GeefMogelijkeAntwoorden();
            ICollection<string> tempString = _mockKlimatogram.Object.GemiddeldeTemperatuur.ToList().ConvertAll(x => x.ToString());
            CollectionAssert.AreEquivalent(tempString.ToList(), v.GeefMogelijkeAntwoorden().ToList());
        }

        [TestMethod]
        public void VraagTemperatuurWarmsteMaandValideertVraagJuist()
        {
            Vraag v = new VraagTemperatuurWarmsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("30.1");
            Assert.AreEqual(Resultaat.Juist, v.Resultaat);
        }

        [TestMethod]
        public void VraagTemperatuurWarmsteMaandValideertVraagFout()
        {
            Vraag v = new VraagTemperatuurWarmsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("25.2");
            Assert.AreEqual(Resultaat.Fout, v.Resultaat);
        }

        [TestMethod]
        public void VraagTemperatuurWarmsteMaandGeeftValidatieTekstBijJuist()
        {
            Vraag v = new VraagTemperatuurWarmsteMaand(_mockKlimatogram.Object);
            string antwoord = "30.1";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is correct.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagTemperatuurWarmsteMaandGeeftValidatieTekstBijFout()
        {
            Vraag v = new VraagTemperatuurWarmsteMaand(_mockKlimatogram.Object);
            string antwoord = "25.2";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is fout.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagTemperatuurWarmsteMaandGeeftVraagTekst()
        {
            Vraag v = new VraagTemperatuurWarmsteMaand(_mockKlimatogram.Object);
            Assert.AreEqual("Hoeveel bedraagt de gemiddelde temperatuur van de warmste maand?", v.GeefVraagTekst());
        }

        #endregion

        #region KoudsteMaandVraagTesten

        [TestMethod]
        public void VraagKoudsteMaandGeeftMogelijkeAntwoorden()
        {
            Vraag v = new VraagKoudsteMaand(_mockKlimatogram.Object);
            ICollection<string> mogelijkeAntwoorden = v.GeefMogelijkeAntwoorden();
            CollectionAssert.AreEqual(_alleMaandenString, mogelijkeAntwoorden.ToList());
        }

        [TestMethod]
        public void VraagKoudsteMaandValideertVraagJuist()
        {
            Vraag v = new VraagKoudsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("Februari");
            Assert.AreEqual(Resultaat.Juist, v.Resultaat);
        }

        [TestMethod]
        public void VraagKoudsteMaandValideertVraagFout()
        {
            Vraag v = new VraagKoudsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("Januari");
            Assert.AreEqual(Resultaat.Fout, v.Resultaat);
        }

        [TestMethod]
        public void VraagKoudsteMaandGeeftValidatieTekstBijJuist()
        {
            Vraag v = new VraagKoudsteMaand(_mockKlimatogram.Object);
            string antwoord = "Februari";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is juist.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagKoudsteMaandGeeftValidatieTekstBijFout()
        {
            Vraag v = new VraagKoudsteMaand(_mockKlimatogram.Object);
            string antwoord = "Januari";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is fout.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagKoudsteMaandGeeftVraagTekst()
        {
            Vraag v = new VraagKoudsteMaand(_mockKlimatogram.Object);
            Assert.AreEqual("Welke is de koudste maand?", v.GeefVraagTekst());
        }

        #endregion

        #region VraagTemperatuurKoudsteMaandTesten

        [TestMethod]
        public void VraagTemperatuurKoudsteMaandGeeftMogelijkeAntwoorden()
        {
            Vraag v = new VraagTemperatuurKoudsteMaand(_mockKlimatogram.Object);
            ICollection<string> mogelijkeAntwoorden = v.GeefMogelijkeAntwoorden();
            ICollection<string> tempString = _mockKlimatogram.Object.GemiddeldeTemperatuur.ToList().ConvertAll(x => x.ToString());
            CollectionAssert.AreEquivalent(tempString.ToList(), v.GeefMogelijkeAntwoorden().ToList());
        }

        [TestMethod]
        public void VraagTemperatuurKoudsteMaandValideertVraagJuist()
        {
            Vraag v = new VraagTemperatuurKoudsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("2.0");
            Assert.AreEqual(Resultaat.Juist, v.Resultaat);
        }

        [TestMethod]
        public void VraagTemperatuurKoudsteMaandValideertVraagFout()
        {
            Vraag v = new VraagTemperatuurKoudsteMaand(_mockKlimatogram.Object);
            v.ValideerVraag("2.2");
            Assert.AreEqual(Resultaat.Fout, v.Resultaat);
        }

        [TestMethod]
        public void VraagTemperatuurKoudsteMaandGeeftValidatieTekstBijJuist()
        {
            Vraag v = new VraagTemperatuurKoudsteMaand(_mockKlimatogram.Object);
            string antwoord = "2.0";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is correct.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagTemperatuurKoudsteMaandGeeftValidatieTekstBijFout()
        {
            Vraag v = new VraagTemperatuurKoudsteMaand(_mockKlimatogram.Object);
            string antwoord = "2.2";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is fout.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagTemperatuurKoudsteMaandGeeftVraagTekst()
        {
            Vraag v = new VraagTemperatuurKoudsteMaand(_mockKlimatogram.Object);
            Assert.AreEqual("Hoeveel bedraagt de gemiddelde temperatuur van de koudste maand?", v.GeefVraagTekst());
        }

        #endregion

        #region VraagAantalDrogeMaandenTesten

        [TestMethod]
        public void VraagAantalDrogeMaandenGeeftMogelijkeAntwoorden()
        {
            Vraag v = new VraagAantalDrogeMaanden(_mockKlimatogram.Object);
            ICollection<string> mogelijkeAntwoorden = v.GeefMogelijkeAntwoorden();
            string[] alleAantallen = { "1" , "2", "3" , "4", "5" , "6", "7" , "8", "9" , "10", "11" , "12" };
            CollectionAssert.AreEquivalent(alleAantallen, v.GeefMogelijkeAntwoorden().ToList());
        }

        [TestMethod]
        public void VraagAantalDrogeMaandenValideertVraagJuist()
        {
            Vraag v = new VraagAantalDrogeMaanden(_mockKlimatogram.Object);
            v.ValideerVraag("9");
            Assert.AreEqual(Resultaat.Juist, v.Resultaat);
        }

        [TestMethod]
        public void VraagAantalDrogeMaandenValideertVraagFout()
        {
            Vraag v = new VraagAantalDrogeMaanden(_mockKlimatogram.Object);
            v.ValideerVraag("7");
            Assert.AreEqual(Resultaat.Fout, v.Resultaat);
        }

        [TestMethod]
        public void VraagAantalDrogeMaandenGeeftValidatieTekstBijJuist()
        {
            Vraag v = new VraagAantalDrogeMaanden(_mockKlimatogram.Object);
            string antwoord = "9";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is correct.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagAantalDrogeMaandenGeeftValidatieTekstBijFout()
        {
            Vraag v = new VraagAantalDrogeMaanden(_mockKlimatogram.Object);
            string antwoord = "7";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is fout.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagAantalDrogeMaandenGeeftVraagTekst()
        {
            Vraag v = new VraagAantalDrogeMaanden(_mockKlimatogram.Object);
            Assert.AreEqual("Hoeveel droge maanden zijn er?", v.GeefVraagTekst());
        }

        #endregion

        #region VraagNeerslagInDeZomerTesten

        [TestMethod]
        public void VraagNeerslagInDeZomerGeeftMogelijkeAntwoorden()
        {
            Vraag v = new VraagNeerslagInDeZomer(_mockKlimatogram.Object);
            ICollection<string> mogelijkeAntwoorden = v.GeefMogelijkeAntwoorden();
            string[] mogelijkeHoeveelheden = { "87", "88" };
            CollectionAssert.AreEquivalent(mogelijkeHoeveelheden, v.GeefMogelijkeAntwoorden().ToList());
        }

        [TestMethod]
        public void VraagNeerslagInDeZomerValideertVraagJuist()
        {
            Vraag v = new VraagNeerslagInDeZomer(_mockKlimatogram.Object);
            v.ValideerVraag("87");
            Assert.AreEqual(Resultaat.Juist, v.Resultaat);
        }

        [TestMethod]
        public void VraagNeerslagInDeZomerValideertVraagFout()
        {
            Vraag v = new VraagNeerslagInDeZomer(_mockKlimatogram.Object);
            v.ValideerVraag("88");
            Assert.AreEqual(Resultaat.Fout, v.Resultaat);
        }

        [TestMethod]
        public void VraagNeerslagInDeZomerGeeftValidatieTekstBijJuist()
        {
            Vraag v = new VraagNeerslagInDeZomer(_mockKlimatogram.Object);
            string antwoord = "87";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is correct.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagNeerslagInDeZomerGeeftValidatieTekstBijFout()
        {
            Vraag v = new VraagNeerslagInDeZomer(_mockKlimatogram.Object);
            string antwoord = "88";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is fout.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagNeerslagInDeZomerGeeftVraagTekst()
        {
            Vraag v = new VraagNeerslagInDeZomer(_mockKlimatogram.Object);
            Assert.AreEqual("Hoeveel bedraagt de totale neerslag in de zomer?", v.GeefVraagTekst());
        }

        #endregion

        #region VraagNeerslagInDeWinterTesten

        [TestMethod]
        public void VraagNeerslagInDeWinterGeeftMogelijkeAntwoorden()
        {
            Vraag v = new VraagNeerslagInDeWinter(_mockKlimatogram.Object);
            ICollection<string> mogelijkeAntwoorden = v.GeefMogelijkeAntwoorden();
            string[] mogelijkeHoeveelheden = { "87", "88" };
            CollectionAssert.AreEquivalent(mogelijkeHoeveelheden, v.GeefMogelijkeAntwoorden().ToList());
        }

        [TestMethod]
        public void VraagNeerslagInDeWinterValideertVraagJuist()
        {
            Vraag v = new VraagNeerslagInDeWinter(_mockKlimatogram.Object);
            v.ValideerVraag("88");
            Assert.AreEqual(Resultaat.Juist, v.Resultaat);
        }

        [TestMethod]
        public void VraagNeerslagInDeWinterValideertVraagFout()
        {
            Vraag v = new VraagNeerslagInDeWinter(_mockKlimatogram.Object);
            v.ValideerVraag("87");
            Assert.AreEqual(Resultaat.Fout, v.Resultaat);
        }

        [TestMethod]
        public void VraagNeerslagInDeWinterGeeftValidatieTekstBijJuist()
        {
            Vraag v = new VraagNeerslagInDeWinter(_mockKlimatogram.Object);
            string antwoord = "88";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is correct.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagNeerslagInDeWinterGeeftValidatieTekstBijFout()
        {
            Vraag v = new VraagNeerslagInDeWinter(_mockKlimatogram.Object);
            string antwoord = "87";
            v.ValideerVraag(antwoord);
            Assert.AreEqual(antwoord + " is fout.", v.GeefValidatieTekst());
        }

        [TestMethod]
        public void VraagNeerslagInDeWinterGeeftVraagTekst()
        {
            Vraag v = new VraagNeerslagInDeWinter(_mockKlimatogram.Object);
            Assert.AreEqual("Hoeveel bedraagt de totale neerslag in de winter?", v.GeefVraagTekst());
        }

        #endregion
    }
}
