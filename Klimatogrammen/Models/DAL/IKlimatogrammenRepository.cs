using System.Collections.Generic;
using System.Linq;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    public interface IKlimatogrammenRepository {
        IQueryable<Continent> GeefContinenten();
        Continent GeefContinent(string naam);
    }
}