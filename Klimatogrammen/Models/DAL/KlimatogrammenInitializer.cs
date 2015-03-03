using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Spatial;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Models.DAL
{
    public class KlimatogrammenInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<KlimatogrammenContext>
    {
        private IEnumerable<Maand> VormMaanden(double[] temperaturen, int[] neerslagen) {
            string[] maanden = new string[] {"Januari","Februari","Maart","April","Mei", "Juni","Juli","Augustus","September","Oktober","November","December"};
            return maanden.Select((t, i) => new Maand(t, temperaturen[i], neerslagen[i]));
        }

        private DeterminatieTabel MaakKleineDeterminatieTabel() {
            //Determinatietabel aanmaken
            DeterminatieTabel tabel = new DeterminatieTabel();

            

            //Nieuwe vergelijking aanmaken (Tw < 10°C)
            Vergelijking vergelijking = new Vergelijking();
            vergelijking.LinkerParameter = _parameterFactory.MaakParameter("Tw");
            vergelijking.RechterParameter = _parameterFactory.MaakConstanteParameter(10);
            vergelijking.Operator = Operator.KleinerDan;

            //Ja knoop aanmaken van de eerste vergelijking
            //Vergelijking aanmaken van de ja knoop (Tw < 0°C)
            Vergelijking vergelijking2 = new Vergelijking();
            vergelijking2.LinkerParameter = _parameterFactory.MaakParameter("Tw");
            vergelijking2.RechterParameter = _parameterFactory.MaakConstanteParameter(0);
            vergelijking2.Operator = Operator.KleinerDan;

            //Resultaatknoop voor ja en nee Tw < 0°C
            DeterminatieKnoop resultaatKnoopJa = new ResultaatKnoop("", "Koud zonder dooiseizoen");
            DeterminatieKnoop resultaatKnoopNee = new ResultaatKnoop("", "Koud met dooiseizoen");

            //Ja knoop instellen van de ja tak van de eerste vergelijking
            DeterminatieKnoop jaKnoop = new BeslissingsKnoop(vergelijking2, resultaatKnoopJa, resultaatKnoopNee);

            //Nee knoop aanmaken van de beginknoop
            Vergelijking vergelijking3 = new Vergelijking();
            vergelijking3.LinkerParameter = _parameterFactory.MaakParameter("Tw4");
            vergelijking3.RechterParameter = _parameterFactory.MaakConstanteParameter(10);
            vergelijking3.Operator = Operator.KleinerDan;

            //Resultaatknoop aanmaken van vergelijking 3
            DeterminatieKnoop resultaatKnoopJa2 = new ResultaatKnoop("", "Koud gematigd");

            //Nee knoop aanmaken van vergelijking 3
            Vergelijking vergelijking4 = new Vergelijking();
            vergelijking4.LinkerParameter = _parameterFactory.MaakParameter("Tk");
            vergelijking4.RechterParameter = _parameterFactory.MaakConstanteParameter(18);
            vergelijking4.Operator = Operator.KleinerDan;

            //Resultaat knoop nee aanmaken van vergelijking 4
            DeterminatieKnoop resultaatKnoopNee2 = new ResultaatKnoop("", "Warm");

            //Vergelijking aanmaken van ja knoop 2
            Vergelijking vergelijking5 = new Vergelijking();
            vergelijking5.LinkerParameter = _parameterFactory.MaakParameter("Nj");
            vergelijking5.RechterParameter = _parameterFactory.MaakConstanteParameter(400);
            vergelijking5.Operator = Operator.GroterDan;

            //Resultaat knoop nee aanmaken van vergelijking 5
            DeterminatieKnoop resultaatKnoopNee4 = new ResultaatKnoop("", "Gematigd en droog");

            //Vergelijking aanmaken van ja knoop 3
            Vergelijking vergelijking6 = new Vergelijking();
            vergelijking6.LinkerParameter = _parameterFactory.MaakParameter("Tk");
            vergelijking6.RechterParameter = _parameterFactory.MaakConstanteParameter(-3);
            vergelijking6.Operator = Operator.KleinerDan;

            //Resultaat knoop aanmaken van vergelijking 6
            DeterminatieKnoop resultaatKnoopJa3 = new ResultaatKnoop("", "Koel gematigd met strenge winter");

            //Vergelijking aanmaken van nee knoop 3
            Vergelijking vergelijking7 = new Vergelijking();
            vergelijking7.LinkerParameter = _parameterFactory.MaakParameter("Tw");
            vergelijking7.RechterParameter = _parameterFactory.MaakConstanteParameter(22);
            vergelijking7.Operator = Operator.KleinerDan;

            //Resultaat knoop aanmaken van vergelijking 7
            DeterminatieKnoop resultaatKnoopJa4 = new ResultaatKnoop("", "Koel gematigd met zachte winter");
            DeterminatieKnoop resultaatKnoopNee3 = new ResultaatKnoop("", "Warm gematigd met natte winter");

            //Nee knoop aanmaken van vergelijking 6
            DeterminatieKnoop neeKnoop3 = new BeslissingsKnoop(vergelijking7, resultaatKnoopJa4, resultaatKnoopNee3);

            //Ja knoop aanmaken van vergelijking 5
            DeterminatieKnoop jaKnoop3 = new BeslissingsKnoop(vergelijking6, resultaatKnoopJa3, neeKnoop3);

            //Ja knoop aanmaken van vergelijking 4
            DeterminatieKnoop jaKnoop2 = new BeslissingsKnoop(vergelijking5, jaKnoop3, resultaatKnoopNee4);

            //Knoop aanmaken van vergelijking 4
            DeterminatieKnoop neeKnoop2 = new BeslissingsKnoop(vergelijking4, jaKnoop2, resultaatKnoopNee2);

            //Knoop aanmaken van vergelijking 3
            DeterminatieKnoop neeKnoop = new BeslissingsKnoop(vergelijking3, resultaatKnoopJa2, neeKnoop2);


            tabel.BeginKnoop = new BeslissingsKnoop(vergelijking, jaKnoop, neeKnoop);

            return tabel;
        }

        private DeterminatieTabel MaakGroteDeterminatieTabel() {
            DeterminatieTabel dt = new DeterminatieTabel();

            ResultaatKnoop jajaResultaatKnoop = new ResultaatKnoop("Ijswoestijnklimaat","Koud klimaat zonder dooiseizoen");
            ResultaatKnoop janeeResultaatKnoop = new ResultaatKnoop("Toendraklimaat","Koud klimaat met dooiseizoen");
            ResultaatKnoop neejaResultaatKnoop = new ResultaatKnoop("Taigaklimaat","Koudgematigd klimaat met strenge winter");
            ResultaatKnoop neeneejajaResultaatKnoop = new ResultaatKnoop("Woestijnklimaat van de middelbreedten","Gematigd altijd droog klimaat");
            ResultaatKnoop neeneejaneeResultaatKnoop = new ResultaatKnoop("Woestijnklmaat van de tropen","Warm altijd droog klimaat");
            ResultaatKnoop neeneeneejajaResultaatKnoop = new ResultaatKnoop("Steppeklimaat","Gematigd, droog klimaat");
            ResultaatKnoop neeneeneejaneejaResultaatKnoop = new ResultaatKnoop("Taigaklimaat","Koudgematigd klimaat met strenge winter");
            ResultaatKnoop neeneeneejaneeneejajaResultaatKnoop = new ResultaatKnoop("Gemengd-woudklimaat","Koelgematigd klimaat met koude winter");
            ResultaatKnoop neeneeneejaneeneejaneejaResultaatKnoop = new ResultaatKnoop("Loofbosklimaat","Koelgematigd klimaat met zachte winter");
            ResultaatKnoop neeneeneejaneeneejaneeneeResultaatKnoop = new ResultaatKnoop("Subtropisch regenwoudklimaat","Warmgematigd altijd nat klimaat");
            ResultaatKnoop neeneeneejaneeneeneejajaResultaatKnoop = new ResultaatKnoop("Hardbladige-vegetatieklimaat van de centrale middelbreedten","Koelgematigd klimaat met natte winter");
            ResultaatKnoop neeneeneejaneeneeneejaneeResultaatKnoop = new ResultaatKnoop("Hardbladige-vegetatieklimaat van de subtropen","Warmgematigd klimaat met natte winter");
            ResultaatKnoop neeneeneejaneeneeneeneeResultaatKnoop = new ResultaatKnoop("Subtropisch savanneklimaat","Warmgematigd klimaat met natte zomer");
            ResultaatKnoop neeneeneeneeneeResultaatKnoop = new ResultaatKnoop("Tropisch savanneklimaat", "Warm klimaat met nat seizoen");
            ResultaatKnoop neeneeneeneejaResultaatKnoop = new ResultaatKnoop("Tropisch regenwoudklimaat","Warm altijd nat klimaat");


            BeslissingsKnoop jaBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tw"),Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(0)), jajaResultaatKnoop,janeeResultaatKnoop );
            BeslissingsKnoop neeneejaBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tk"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(15)),neeneejajaResultaatKnoop,neeneejaneeResultaatKnoop );
            BeslissingsKnoop neeneeneeneeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("D"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(1)),neeneeneeneejaResultaatKnoop, neeneeneeneeneeResultaatKnoop );
            BeslissingsKnoop neeneeneejaneeneeneejaBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tw"),Operator.KleinerDanOfGelijkAan,_parameterFactory.MaakConstanteParameter(22)),neeneeneejaneeneeneejajaResultaatKnoop, neeneeneejaneeneeneejaneeResultaatKnoop );
            BeslissingsKnoop neeneeneejaneeneeneeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Nz"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakParameter("Nw")), neeneeneejaneeneeneejaBeslissingsKnoop,neeneeneejaneeneeneeneeResultaatKnoop );
            BeslissingsKnoop neeneeneejaneeneejaneeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tw"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(22)), neeneeneejaneeneejaneejaResultaatKnoop,neeneeneejaneeneejaneeneeResultaatKnoop );
            BeslissingsKnoop neeneeneejaneeneejaBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tk"),Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(-3)),neeneeneejaneeneejajaResultaatKnoop, neeneeneejaneeneejaneeBeslissingsKnoop );
            BeslissingsKnoop neeneeneejaneeneeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("D"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(1)),neeneeneejaneeneejaBeslissingsKnoop, neeneeneejaneeneeneeBeslissingsKnoop );
            BeslissingsKnoop neeneeneejaneeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tk"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(-10)), neeneeneejaneejaResultaatKnoop, neeneeneejaneeneeBeslissingsKnoop );
            BeslissingsKnoop neeneeneejaBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Nj"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(400)),neeneeneejajaResultaatKnoop,neeneeneejaneeBeslissingsKnoop);
            BeslissingsKnoop neeneeneeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tk"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(18)), neeneeneejaBeslissingsKnoop, neeneeneeneeBeslissingsKnoop);
            BeslissingsKnoop neeneeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Nj"),Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(200)), neeneejaBeslissingsKnoop, neeneeneeBeslissingsKnoop );
            BeslissingsKnoop neeBeslissingsKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tj"), Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(0)),neejaResultaatKnoop,neeneeBeslissingsKnoop );
            BeslissingsKnoop hoofdKnoop = new BeslissingsKnoop(new Vergelijking(_parameterFactory.MaakParameter("Tw"),Operator.KleinerDanOfGelijkAan, _parameterFactory.MaakConstanteParameter(10)),jaBeslissingsKnoop,neeBeslissingsKnoop );

            dt.BeginKnoop = hoofdKnoop;

            return dt;
        }
        private  ParameterFactory _parameterFactory = new ParameterFactory();
        protected override void Seed(KlimatogrammenContext context)
        {
            try
            {
                Graad eersteGraad = new Graad() {Nummer = 1};
                Graad tweedeGraadEersteJaar = new Graad() {Nummer = 2, Jaar = 1};
                Graad tweedeGraadTweedeJaar = new Graad() {Nummer = 2, Jaar = 2};
                Graad derdeGraad = new Graad() {Nummer =3};
      
                eersteGraad.Vragen = new Collection<Vraag>();
                eersteGraad.Vragen.Add(new Vraag() {Parameter = _parameterFactory.MaakParameter("Warmste Maand"), VraagTekst = "Wat is de warmste maand?"});
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Tw"), VraagTekst = "Wat is de temperatuur van de warmste maand (Tw)?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Koudste Maand"), VraagTekst = "Wat is de koudste maand?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Tk"), VraagTekst = "Wat is de temperatuur van de koudste maan (Tk)?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("D"), VraagTekst = "Hoeveel droge maanden zijn er (D)?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Nz"), VraagTekst = "Hoeveelheid neerslag in de zomer?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Nw"), VraagTekst = "Hoeveelheid neerslag in de winter?" });

                //TODO: Graad 1 krijgt kleine tabel, graad 2 krijgt grote
                DeterminatieTabel klein = MaakKleineDeterminatieTabel();
                DeterminatieTabel groot = MaakGroteDeterminatieTabel();

                eersteGraad.DeterminatieTabel = klein;
                tweedeGraadEersteJaar.DeterminatieTabel =
                    tweedeGraadTweedeJaar.DeterminatieTabel = derdeGraad.DeterminatieTabel = groot;

                Continent noordAmerika = new Continent("Noord-Amerika");
                Continent zuidAmerika = new Continent("Zuid-Amerika");
                Continent antartica = new Continent("Antartica");
                Continent europa = new Continent("Europa");
                Continent azie = new Continent("Azië");
                Continent afrika = new Continent("Afrika");
                Continent oceanie = new Continent("Oceanië");
                #region "data Continenten"

                Land belgie = new Land("België");
                
                    
                int[] gemiddeldeNeerUkkel = { 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17 };
                double[] gemiddeldeTempUkkel = { 5.1, 2.2, 10.5, 12.7, 18, 20.4, 21.2, 25.2, 30.1, 19, 10, 2.2 };
                belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(gemiddeldeTempUkkel, gemiddeldeNeerUkkel).ToList(), 50.8, 4.35) { Locatie = "Ukkel", BeginJaar = 1961, EindJaar = 2009 });
                europa.VoegLandToe(belgie);



                Land griekenland = new Land("Griekenland");
                griekenland.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { 10.0, 10.5, 12.4, 16.0, 20.6, 25.0, 27.8, 24.3, 19.3, 15.4, 12.0, 9.0 },
                new int[] { 45, 48, 44, 25, 14, 6, 6, 8, 10, 48, 51, 66 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Athene" });

                Land kameroen = new Land("Kameroen");
                kameroen.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[]  { 23.7, 25.3, 25.0, 24.6, 24.1, 23.4, 22.6, 23.0, 23.1, 23.3, 23.7, 23.7 },
                new int[] { 17, 51, 140, 180, 220, 162, 70, 102, 254, 296, 111, 25 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Yaounde" });

                Land brazillie = new Land("Brazillië");
                brazillie.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { 21.6, 21.8, 22.0, 21.4, 20.2, 19.1, 19.1, 21.2, 22.5, 22.1, 21.7, 21.5 },
                new int[] { 241, 215, 189, 124, 39, 9, 12, 13, 52, 172, 238, 249 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Brasilia" });

                Land australie = new Land("Australië");
                australie.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { 24.2, 24.6, 22.7, 19.2, 16.0, 13.9, 13.0, 13.2, 14.5, 16.3, 19.1, 21.8 },
                new int[] { 9, 14, 16, 47, 103, 168, 156, 111, 71, 46, 23, 9 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Perth" });

                Land thailand = new Land("Thailand");
                thailand.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { 25.9, 27.4, 28.7, 29.7, 29.2, 28.7, 28.3, 28.1, 27.8, 27.6, 26.9, 25.6 },
                new int[] { 9, 30, 29, 65, 220, 149, 155, 197, 344, 242, 48, 10 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Bangkok" });
                Land witRusland = new Land("Wit-Rusland");
                witRusland.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { -6.9, -5.8, -1.4, 6.0, 12.9, 16.1, 17.3, 16.5, 11.7, 6.3, 0.8, -3.8 },
                new int[] { 40, 34, 42, 42, 62, 83, 88, 72, 60, 49, 52, 53 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Minsk" });

                Land rusland = new Land("Rusland");
                rusland.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { -41.0, -36.0, -22.0, -6.1, 6.7, 15.4, 18.7, 14.9, 5.7, -8.5, -29, -39 },
                new int[] { 9, 7, 6, 10, 18, 37, 39, 37, 29, 20, 16, 12 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Jakutsk" });
                Land vs = new Land("Usa");
                vs.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { -26.0, -27.7, -26.0, -18.8, -6.9, 1.3, 3.9, 2.2, -0.7, -10.1, -18.9, -23.7 },
                new int[] { 4, 4, 5, 5, 5, 7, 25, 23, 14, 11, 6, 5 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Barrow, Alaska" });

                vs.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[]  { -2.3, 1.2, 5.4, 9.8, 14.9, 20.6, 25.5, 24.2, 18.4, 11.8, 4.9, -1.3 },
                new int[] { 28, 31, 49, 54, 46, 24, 21, 22, 33, 37, 33, 36 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1951, EindJaar = 1990, Locatie = "Salt Lake City" });

                australie.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[]  { 28.7, 27.8, 24.9, 20.2, 15.6, 12.4, 11.6, 14.2, 18.2, 22.8, 25.7, 27.8 },
                new int[] { 42, 3, 52, 17, 18, 14, 16, 12, 11, 19, 27, 36 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Alice Springs" });

                europa.VoegLandToe(griekenland);
                europa.VoegLandToe(witRusland);
                azie.VoegLandToe(rusland);
                azie.VoegLandToe(thailand);
                noordAmerika.VoegLandToe(vs);
                afrika.VoegLandToe(kameroen);
                zuidAmerika.VoegLandToe(brazillie);
                oceanie.VoegLandToe(australie);

                #endregion
                eersteGraad.Continenten =new Collection<Continent>();
                eersteGraad.Continenten.Add(europa);
                var contintent = new[] {europa, antartica, noordAmerika, zuidAmerika, azie, afrika, oceanie};

                tweedeGraadEersteJaar.Continenten = new Collection<Continent>();
                tweedeGraadTweedeJaar.Continenten = new Collection<Continent>();
                derdeGraad.Continenten = new Collection<Continent>();
                foreach (var item in contintent) {
                    tweedeGraadEersteJaar.Continenten.Add(item);
                    tweedeGraadTweedeJaar.Continenten.Add(item);
                    derdeGraad.Continenten.Add(item);
                }
                context.Graden.AddRange(new[] { eersteGraad, tweedeGraadEersteJaar,tweedeGraadTweedeJaar,derdeGraad });
                context.SaveChanges();



            }
            catch (DbEntityValidationException e)
            {
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Graad: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }
    }
}