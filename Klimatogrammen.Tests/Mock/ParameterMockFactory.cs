using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klimatogrammen.Models.Domein;
using Moq;

namespace Klimatogrammen.Tests.Mock
{
    public class ParameterMockFactory
    {
        public Mock<Parameter> MaakParameterMock()
        {
            
            Mock<Parameter> mock = new Mock<Parameter>();
            mock.Setup(p => p.GeefBeschrijving()).Returns("Gemockte parameter");
            mock.Setup(p => p.GeefMogelijkeAntwoorden(It.IsAny<Klimatogram>()))
                .Returns(Enumerable.Range(1, 3).Select(i => "Item" + i).ToList());
            mock.Setup(p => p.BerekenWaarde(It.IsAny<Klimatogram>())).Returns("Item2");
            return mock;
        }
    }
}
