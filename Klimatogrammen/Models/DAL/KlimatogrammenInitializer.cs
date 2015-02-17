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
                Land belgie = new Land("België");
                europa.Landen.Add(belgie);
                europa.Landen.Add(new Land("Frankrijk"));
                europa.Landen.Add(new Land("Duitsland"));
                europa.Landen.Add(new Land("Spanje"));
                noordAmerika.Landen.Add(new Land("Usa"));
                noordAmerika.Landen.Add(new Land("Canada"));
                zuidAmerika.Landen.Add(new Land("Mexico"));
                zuidAmerika.Landen.Add(new Land("Peru"));
                antartica.Landen.Add(new Land("Niemandsland"));
                azie.Landen.Add(new Land("China"));
                azie.Landen.Add(new Land("Japan"));
                afrika.Landen.Add(new Land("Egypte"));
                afrika.Landen.Add(new Land("Congo"));
                afrika.Landen.Add(new Land("Zimbabwe"));
                oceanie.Landen.Add(new Land("Nieuw Zeeland"));
                oceanie.Landen.Add(new Land("Australië"));
                Neerslag[] gemiddeldeNeer = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
                Temperatuur[] gemiddeldeTemp = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
                belgie.Klimatogrammen.Add(new Klimatogram(gemiddeldeTemp,gemiddeldeNeer) {Locatie="Ukkel"});
                context.Continenten.AddRange(new []
                {noordAmerika, zuidAmerika, antartica, europa, azie, afrika, oceanie});
                context.SaveChanges();

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