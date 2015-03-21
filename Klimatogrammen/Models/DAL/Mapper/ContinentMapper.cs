using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    /// <summary>
    /// Mapper die de continenttabel opmaakt en de relaties en primaire sleutels instelt
    /// </summary>
    public class ContinentMapper : EntityTypeConfiguration<Continent> {
        public ContinentMapper() {
            ToTable("continenten");
            HasKey(c => c.Naam);
            Property(c => c.Naam).IsRequired();
        }
    }
}