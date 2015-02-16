using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class LandMapper : EntityTypeConfiguration<Land> {
        public LandMapper() {
            HasKey(l => l.Naam);
        }
    }
}