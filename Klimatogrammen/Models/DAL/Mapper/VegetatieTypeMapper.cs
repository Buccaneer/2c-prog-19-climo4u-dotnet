using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL.Mapper
{ /// <summary>
    /// Mapper die de vegetatietypetabel opmaakt
    /// </summary>
    public class VegetatieTypeMapper:EntityTypeConfiguration<VegetatieType>
    {
        public VegetatieTypeMapper()
        {
            ToTable("vegetatieType");
        }
    }
}