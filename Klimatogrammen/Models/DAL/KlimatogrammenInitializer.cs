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
          
                Neerslag[] gemiddeldeNeerUkkel = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
                Temperatuur[] gemiddeldeTempUkkel = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
                belgie.VoegKlimatogramToe(new Klimatogram(gemiddeldeTempUkkel,gemiddeldeNeerUkkel) {Locatie="Ukkel", BeginJaar = 1961, EindJaar = 2009});
                europa.VoegLandToe(belgie);

                Land griekenland = new Land("Griekenland");
                griekenland.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] {10.0,10.5,12.4,16.0,20.6,25.0,27.8,24.3,19.3,15.4,12.0,9.0},
                new Neerslag[]{45,48,44,25,14,6,6,8,10,48,51,66
                }
            ) { BeginJaar = 1961,EindJaar = 1990, Locatie="Athene"});

                Land kameroen = new Land("Kameroen");
                kameroen.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { 23.7,25.3,25.0,24.6,24.1,23.4,22.6,23.0,23.1,23.3,23.7, 23.7 },
                new Neerslag[]{17,51,140,180,220,162,70,102,254,296,111,25
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Yaounde" });

                Land brazillie = new Land("Brazillië");
                brazillie.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { 21.6,21.8,22.0,21.4,20.2,19.1,19.1,21.2,22.5,22.1,21.7,21.5 },
                new Neerslag[]{241,215,189,124,39,9,12,13,52,172,238,249
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Brasilia" });

                Land australie = new Land("Australië");
                australie.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { 24.2,24.6,22.7,19.2,16.0,13.9,13.0,13.2,14.5,16.3,19.1,21.8 },
                new Neerslag[]{9,14,16,47,103,168,156,111,71,46,23,9
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Perth" });

                Land thailand = new Land("Thailand");
                thailand.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { 25.9,27.4,28.7,29.7,29.2,28.7,28.3,28.1,27.8,27.6,26.9,25.6},
                new Neerslag[]{9,30,29,65,220,149,155,197,344,242,48,10
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Bangkok" });
                Land witRusland = new Land("Wit-Rusland");
                witRusland.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { -6.9,-5.8,-1.4,6.0,12.9,16.1,17.3,16.5,11.7,6.3,0.8,-3.8 },
                new Neerslag[]{40,34,42,42,62,83,88,72,60,49,52,53
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Minsk" });

                Land rusland = new Land("Rusland");
                rusland.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { -41.0,-36.0,-22.0,-6.1,6.7,15.4,18.7,14.9,5.7,-8.5,-29,-39 },
                new Neerslag[]{9,7,6,10,18,37,39,37,29,20,16,12
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Jakutsk" });
                Land vs = new Land("Usa");
                vs.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { -26.0,-27.7,-26.0,-18.8,-6.9,1.3,3.9,2.2,-0.7,-10.1,-18.9,-23.7 },
                new Neerslag[]{4,4,5,5,5,7,25,23,14,11,6,5
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Barrow, Alaska" });
         
                vs.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] {-2.3,1.2,5.4,9.8,14.9,20.6,25.5,24.2,18.4,11.8,4.9,-1.3},
                new Neerslag[]{28,31,49,54,46,24,21,22,33,37,33,36
                }
            ) { BeginJaar = 1951, EindJaar = 1990, Locatie = "Salt Lake City" });

                australie.VoegKlimatogramToe(new Klimatogram(
                new Temperatuur[] { 28.7,27.8,24.9,20.2,15.6,12.4,11.6,14.2,18.2,22.8,25.7,27.8 },
                new Neerslag[]{42,3,52,17,18,14,16,12,11,19,27,36
                }
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Alice Springs" });

                europa.VoegLandToe(griekenland);
                europa.VoegLandToe(witRusland);
                azie.VoegLandToe(rusland);
                azie.VoegLandToe(thailand);
                noordAmerika.VoegLandToe(vs);
                afrika.VoegLandToe(kameroen);
                zuidAmerika.VoegLandToe(brazillie);
                oceanie.VoegLandToe(australie);
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