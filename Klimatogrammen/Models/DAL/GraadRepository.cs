using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    /// <summary>
    /// De GraadRepository geeft een graad door, afhankelijk van de graad en het jaar dat de methode 'GeefGraad' meekrijgt
    /// </summary>
    public class GraadRepository : IGraadRepository{
        private KlimatogrammenContext _context;
        private DbSet<Graad> _graden;
        public Graad GeefGraad(int graad, int jaar) {
            var result = _graden
                .Include(g => g.Vragen.Select(v => v.Parameter))
                .Include(g => g.DeterminatieTabel.BeginKnoop)
                
                .FirstOrDefault(g => g.Nummer == graad && g.Jaar == jaar);
            result.DeterminatieTabel.BeginKnoop.Laad();
            return result;

        }

        public GraadRepository(KlimatogrammenContext context) {
            _context = context;
            _graden = context.Graden;
        }
    }
}