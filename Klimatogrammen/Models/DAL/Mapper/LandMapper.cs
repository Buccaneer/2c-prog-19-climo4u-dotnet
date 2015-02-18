using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class LandMapper : EntityTypeConfiguration<Land> {
        public LandMapper() {
            ToTable("landen");
            HasKey(l => l.Naam);
            Property(l => l.Naam).HasMaxLength(40);
            HasMany(l => l.Klimatogrammen).WithRequired(k => k.Land);
        }
    }
}