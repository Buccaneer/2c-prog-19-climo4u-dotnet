using System;
using Klimatogrammen.Models.Domein;
using Klimatogrammen.Tests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Klimatogrammen.Tests.Models
{
    [TestClass]
    public class VergelijkingTest
    {
        private Mock<Klimatogram> _klimatogramMock;

        [TestInitialize]
        public void Init()
        {
            _klimatogramMock = new KlimatogramMockFactory().MaakKlimatogramMock();
        }


        [TestMethod]
        public void OperatorKleinerDanGeeftCorrecteBoolTerug() 
        {
            Vergelijking vergelijking = new Vergelijking(new ParameterTemperatuurKoudsteMaand(), Operator.KleinerDan, new ConstanteParameter(0));
            bool oplossing = vergelijking.BerekenResultaat(_klimatogramMock.Object);
            Assert.IsFalse(oplossing);

        }
        [TestMethod]
        public void OperatorGroterDanGeeftCorrecteBoolTerug()
        {
            Vergelijking vergelijking = new Vergelijking(new ParameterTemperatuurWarmsteMaand(), Operator.GroterDan, new ConstanteParameter(20));
            bool oplossing = vergelijking.BerekenResultaat(_klimatogramMock.Object);
            Assert.IsTrue(oplossing);

        }
        [TestMethod]
        public void OperatorKleinerDanOfGelijkAanGeeftCorrecteBoolTerug()
        {
            Vergelijking vergelijking = new Vergelijking(new ParameterTemperatuurKoudsteMaand(), Operator.KleinerDanOfGelijkAan, new ConstanteParameter(15));
            bool oplossing = vergelijking.BerekenResultaat(_klimatogramMock.Object);
            Assert.IsTrue(oplossing);

        }
        [TestMethod]
        public void OperatorGroterDanOfGelijkAanGeeftCorrecteBoolTerug()
        {
            Vergelijking vergelijking = new Vergelijking(new ParameterTemperatuurKoudsteMaand(), Operator.GroterDanOfGelijkAan, new ConstanteParameter(12));
            bool oplossing = vergelijking.BerekenResultaat(_klimatogramMock.Object);
            Assert.IsFalse(oplossing);

        }
        [TestMethod]
        public void OperatorNietGelijkAanGeeftCorrecteBoolTerug()
        {
            Vergelijking vergelijking = new Vergelijking(new ParameterTemperatuurKoudsteMaand(), Operator.NietGelijkAan, new ConstanteParameter(0));
            bool oplossing = vergelijking.BerekenResultaat(_klimatogramMock.Object);
            Assert.IsTrue(oplossing);

        }
    }
}
