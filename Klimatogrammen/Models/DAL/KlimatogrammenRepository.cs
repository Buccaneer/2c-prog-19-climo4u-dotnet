using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    public class KlimatogrammenRepository : IKlimatogrammenRepository {
        private KlimatogrammenContext _context;
        private DbSet<Continent> _continenten;

        public KlimatogrammenRepository(KlimatogrammenContext context) {
            _context = context;
            _continenten = context.Continenten;
        }

        public IQueryable<Continent> GeefContinenten() {
            return _continenten.Distinct();
        }

        public Continent GeefContinent(string continent) {
            return _continenten.Include(c => c.Landen).FirstOrDefault(c => c.Naam == continent);
        }
    }
}