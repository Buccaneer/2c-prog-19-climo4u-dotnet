using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    /// <summary>
    /// Mapper die de parametertabel opmaakt en de relaties en primaire sleutels instelt
    /// </summary>
    public class ParameterMapper : EntityTypeConfiguration<Parameter> {
        public ParameterMapper() {
            ToTable("parameters");
            
        }
    }
}