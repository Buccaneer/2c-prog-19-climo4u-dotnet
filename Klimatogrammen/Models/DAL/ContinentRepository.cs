using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    public class ContinentRepository : IContinentRepository {
        private KlimatogrammenContext _context;
        private DbSet<Continent> _continenten;

  
        public ContinentRepository(KlimatogrammenContext context) {
            _context = context;
            _continenten = context.Continenten;
        }

        /// <summary>
        /// Geeft een lijst van continenten terug.
        /// </summary>
        /// <returns>Een IQueryable van continenten</returns>
        public IQueryable<Continent> GeefContinenten() {
            return _continenten.Distinct();
        }

        /// <summary>
        /// Geef een specifiek continent terug met daarin al zijn landen.
        /// </summary>
        /// <param name="continent">naam van het continent</param>
        /// <returns>Spicifiek continent met alle landen geladen.</returns>
        public Continent GeefContinent(string continent) {
            return _continenten.Include(c => c.Landen).FirstOrDefault(c => c.Naam== continent);
        }
    }
}