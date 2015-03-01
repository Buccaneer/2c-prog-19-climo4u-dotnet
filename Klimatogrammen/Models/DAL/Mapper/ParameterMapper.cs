using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class ParameterMapper : EntityTypeConfiguration<Parameter> {
        public ParameterMapper() {
            ToTable("parameters");
            
        }
    }
}