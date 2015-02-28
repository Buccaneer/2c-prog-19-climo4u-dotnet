using System.Collections;
using System.Collections.Generic;
using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Tests.Mock
{
    class GraadRepositoryMock : IGraadRepository
    {
        
        public Graad GeefGraad(int graad, int jaar)
        {
            var factory = new GraadMockFactory();
            switch (graad)
            {
                case 1:
                    return factory.MaakEersteGraadAan().Object;
                case 2:
                    return jaar == 1 ? factory.MaakTweedeGraadEersteJaarAan().Object : factory.MaakTweedeGraadTweedeJaarAan().Object;
                case 3:
                    return factory.MaakDerdeGraadAan().Object;
                default:
                    return null;
            }
        }
    }
}
