using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL {
    public class KlimatogrammenInitializer : System.Data.Entity.DropCreateDatabaseAlways<KlimatogrammenContext> {
        protected override void Seed(KlimatogrammenContext context) {
            try {
                Continent noordAmerika = new Continent("Noord-Amerika");
                Continent zuidAmerika = new Continent("Zuid-Amerika");
                Continent antartica = new Continent("Antartica");
                Continent europa = new Continent("Europa");
                Continent azie = new Continent("Azië");
                Continent afrika = new Continent("Afrika");
                Continent oceanie = new Continent("Oceanië");
                context.Continenten.AddRange(new []
                {noordAmerika, zuidAmerika, antartica, europa, azie, afrika, oceanie});
                context.SaveChanges();
                InitializeDatabase(context);

            } catch (DbEntityValidationException e) {
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors) {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors) {
                        s += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }
    }
}