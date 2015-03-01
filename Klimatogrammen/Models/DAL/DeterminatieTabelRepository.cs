using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    public class DeterminatieTabelRepository : IDeterminatieTabelRepository {
        private KlimatogrammenContext _context;
        private DbSet<DeterminatieTabel> _determinatieTabelen;
        public DeterminatieTabel GeefTabel(string naam) {
            return _determinatieTabelen.Find(naam);
        }

        
        public DeterminatieTabelRepository(KlimatogrammenContext context) {
            _context = context;
            _determinatieTabelen = context.DeterminatieTabel;
        }
    }
}