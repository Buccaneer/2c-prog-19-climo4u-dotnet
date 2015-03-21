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
    /// <summary>
    /// Deze klimatogrammenContext geeft stelt de naam van de database in en geeft wat instellingen door met de OnModelCreating methode
    /// </summary>
    public class KlimatogrammenContext : DbContext {

        public KlimatogrammenContext()
            : base("Projecten2Groep19_db")
        {
            
        }

        public DbSet<Graad> Graden { get; set; }

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