using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class GraadMapper : EntityTypeConfiguration<Graad> {

        public GraadMapper() {
            ToTable("graden");
            HasKey(g => new {g.Nummer, g.Jaar});
         //   Ignore(g => g.Vragen);
            HasMany(g => g.Continenten).WithMany();
        }
    }
}