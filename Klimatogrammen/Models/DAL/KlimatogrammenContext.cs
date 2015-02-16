using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    public class KlimatogrammenContext : DbContext {

        public KlimatogrammenContext() : base("Klimatogrammen") {
            
        }

        public DbSet<Continent> Continenten { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static KlimatogrammenContext Create() {
            return DependencyResolver.Current.GetService<KlimatogrammenContext>();
        }

    }
}