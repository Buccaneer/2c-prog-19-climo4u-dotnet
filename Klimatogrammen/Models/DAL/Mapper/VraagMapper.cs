using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    /// <summary>
    /// Mapper die de vraagtabel opmaakt
    /// </summary>
    public class VraagMapper : EntityTypeConfiguration<Vraag> {
        public VraagMapper() {
            ToTable("vragen");
        }
    }
}