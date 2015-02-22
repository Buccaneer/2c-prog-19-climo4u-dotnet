using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class KlimatogramMapper : EntityTypeConfiguration<Klimatogram> {
        public KlimatogramMapper() {
            ToTable("klimatogrammen");
            HasKey(k => k.Locatie);
            Property(k => k.Locatie).IsRequired().HasMaxLength(40);

        }
    }
}