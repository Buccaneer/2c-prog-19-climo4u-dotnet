using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    public class GraadRepository : IGraadRepository{
        private KlimatogrammenContext _context;
        private DbSet<Graad> _graden;
        public Graad GeefGraad(int graad, int jaar) {
            return _graden
                .Include(g => g.Vragen.Select(v => v.Parameter))
                .Include(g => g.DeterminatieTabel.BeginKnoop)
                .FirstOrDefault(g => g.Nummer == graad && g.Jaar == jaar);
        }

        public GraadRepository(KlimatogrammenContext context) {
            _context = context;
            _graden = context.Graden;
        }
    }
}