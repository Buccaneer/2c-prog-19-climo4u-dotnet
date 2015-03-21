using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    /// <summary>
    /// Mapper die de graadtabel opmaakt en de relaties en primaire sleutels instelt
    /// </summary>
    public class GraadMapper : EntityTypeConfiguration<Graad> {

        public GraadMapper() {
            ToTable("graden");
            HasKey(g => new {g.Nummer, g.Jaar});
            HasMany(g => g.Continenten).WithMany();
        }
    }
}