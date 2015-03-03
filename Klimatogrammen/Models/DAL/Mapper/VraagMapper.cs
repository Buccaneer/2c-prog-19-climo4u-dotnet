using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class VraagMapper : EntityTypeConfiguration<Vraag> {
        public VraagMapper() {
            ToTable("vragen");
            Ignore(v => v.Resultaat);
        }
    }
}