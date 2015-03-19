using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klimatogrammen.Models.Domein;
using Moq;

namespace Klimatogrammen.Tests.Mock
{
    class LeerlingMock : Leerling
    {
        public Mock<Leerling> maakJuisteLeerling()
        {
            var mock = new Mock<Leerling>();
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            Klimatogram klimatogram = kmf.MaakKlimatogramMock().Object;
            GraadMockFactory gmf = new GraadMockFactory();
            Graad graad = gmf.MaakDerdeGraadAan().Object;

            mock.Setup(l => l.GeefKlimatogrammenDerdeGraad()).Returns(new Collection<Klimatogram>(new[] { klimatogram }));
            mock.Setup(l => l.Graad).Returns(graad);
            mock.Setup(l => l.FoutieveKlimatogrammenDerdeJaar).Returns(new Collection<Klimatogram>());


            return mock;

        }

        public Mock<Leerling> maakFouteLeerling()
        {
            var mock = new Mock<Leerling>();
            KlimatogramMockFactory kmf = new KlimatogramMockFactory();
            Klimatogram klimatogram = kmf.MaakKlimatogramMock().Object;
            GraadMockFactory gmf = new GraadMockFactory();
            Graad graad = gmf.MaakDerdeGraadAan().Object;

            mock.Setup(l => l.GeefKlimatogrammenDerdeGraad()).Returns(new Collection<Klimatogram>(new[] { klimatogram }));
            mock.Setup(l => l.Graad).Returns(graad);
            mock.Setup(l => l.FoutieveKlimatogrammenDerdeJaar).Returns(new Collection<Klimatogram>(new[] { klimatogram }));

            return mock;

        }
    }
}
