﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Klimatogrammen.Models.DAL.Mapper {
    public class MaandMapper : EntityTypeConfiguration<Maand> {

        public MaandMapper() {
            ToTable("maanden");
            
            Property(m => m.Naam).HasMaxLength(15).IsRequired();
        }
    }
}