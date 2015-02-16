using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class KlimatogramMapper : EntityTypeConfiguration<Klimatogram> {
        public KlimatogramMapper() {
            HasKey(k => k.Locatie);

            Property(k => k.GemiddeldeNeerslag).
        }
    }
}