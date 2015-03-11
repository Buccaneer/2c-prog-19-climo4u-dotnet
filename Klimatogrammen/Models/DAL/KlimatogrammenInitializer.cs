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
            DeterminatieKnoop resultaatKnoopJa = new ResultaatBlad(new VegetatieType("Koud zonder dooiseizoen vegetatietype",""), "Koud zonder dooiseizoen");
            DeterminatieKnoop resultaatKnoopNee = new ResultaatBlad(new VegetatieType("Koud met dooiseizoen vegetatietype", ""), "Koud met dooiseizoen");

            //Ja knoop instellen van de ja tak van de eerste vergelijking
            DeterminatieKnoop jaKnoop = new BeslissingsKnoop(vergelijking2, resultaatKnoopJa, resultaatKnoopNee);

            //Nee knoop aanmaken van de beginknoop
            Vergelijking vergelijking3 = new Vergelijking();
            vergelijking3.LinkerParameter = _parameterFactory.MaakParameter("Tw4");
            vergelijking3.RechterParameter = _parameterFactory.MaakConstanteParameter(10);
            vergelijking3.Operator = Operator.KleinerDan;

            //Resultaatknoop aanmaken van vergelijking 3
            DeterminatieKnoop resultaatKnoopJa2 = new ResultaatBlad(new VegetatieType("Koud gematigd vegetatietype", ""), "Koud gematigd");

            //Nee knoop aanmaken van vergelijking 3
            Vergelijking vergelijking4 = new Vergelijking();
            vergelijking4.LinkerParameter = _parameterFactory.MaakParameter("Tk");
            vergelijking4.RechterParameter = _parameterFactory.MaakConstanteParameter(18);
            vergelijking4.Operator = Operator.KleinerDan;

            //Resultaat knoop nee aanmaken van vergelijking 4
            DeterminatieKnoop resultaatKnoopNee2 = new ResultaatBlad(new VegetatieType("Warm vegetatietype", ""), "Warm");

            //Vergelijking aanmaken van ja knoop 2
            Vergelijking vergelijking5 = new Vergelijking();
            vergelijking5.LinkerParameter = _parameterFactory.MaakParameter("Nj");
            vergelijking5.RechterParameter = _parameterFactory.MaakConstanteParameter(400);
            vergelijking5.Operator = Operator.GroterDan;

            //Resultaat knoop nee aanmaken van vergelijking 5
            DeterminatieKnoop resultaatKnoopNee4 = new ResultaatBlad(new VegetatieType("Gematigd en droog vegetatietype", ""), "Gematigd en droog");

            //Vergelijking aanmaken van ja knoop 3
            Vergelijking vergelijking6 = new Vergelijking();
            vergelijking6.LinkerParameter = _parameterFactory.MaakParameter("Tk");
            vergelijking6.RechterParameter = _parameterFactory.MaakConstanteParameter(-3);
            vergelijking6.Operator = Operator.KleinerDan;

            //Resultaat knoop aanmaken van vergelijking 6
            DeterminatieKnoop resultaatKnoopJa3 = new ResultaatBlad(new VegetatieType("Koel gematigd met strenge winter vegetatietype", ""), "Koel gematigd met strenge winter");

            //Vergelijking aanmaken van nee knoop 3
            Vergelijking vergelijking7 = new Vergelijking();
            vergelijking7.LinkerParameter = _parameterFactory.MaakParameter("Tw");
            vergelijking7.RechterParameter = _parameterFactory.MaakConstanteParameter(22);
            vergelijking7.Operator = Operator.KleinerDan;

            //Resultaat knoop aanmaken van vergelijking 7
            DeterminatieKnoop resultaatKnoopJa4 = new ResultaatBlad(new VegetatieType("Koel gematigd met zachte winter vegetatietype", ""), "Koel gematigd met zachte winter");
            DeterminatieKnoop resultaatKnoopNee3 = new ResultaatBlad(new VegetatieType("Warm gematigd met natte winter vegetatietype", ""), "Warm gematigd met natte winter");

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

            ResultaatBlad jajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Ijswoestijnklimaat", "http://upload.wikimedia.org/wikipedia/commons/b/bd/AntarcticaDomeCSnow.jpg"), "Koud klimaat zonder dooiseizoen");
            ResultaatBlad janeeResultaatKnoop = new ResultaatBlad(new VegetatieType("Toendraklimaat", "http://upload.wikimedia.org/wikipedia/commons/8/87/Tundra_in_Siberia.jpg"), "Koud klimaat met dooiseizoen");
            ResultaatBlad neejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Taigaklimaat", "http://upload.wikimedia.org/wikipedia/commons/2/2d/Picea_glauca_taiga.jpg"), "Koudgematigd klimaat met strenge winter");
            ResultaatBlad neeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Woestijnklimaat van de middelbreedten", "http://upload.wikimedia.org/wikipedia/commons/a/ae/Snow_Comes_to_the_Atacama_Desert.jpg"), "Gematigd altijd droog klimaat");
            ResultaatBlad neeneejaneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Woestijnklimaat van de tropen", "http://upload.wikimedia.org/wikipedia/commons/2/29/Thorn_Tree_Sossusvlei_Namib_Desert_Namibia_Luca_Galuzzi_2004a.JPG"), "Warm altijd droog klimaat");
            ResultaatBlad neeneeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Steppeklimaat", "http://upload.wikimedia.org/wikipedia/commons/d/d5/2013-07-04_15_37_14_Sagebrush-steppe_along_U.S._Route_93_in_central_Elko_County_in_Nevada.jpg"), "Gematigd, droog klimaat");
            ResultaatBlad neeneeneejaneejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Taigaklimaat", "http://upload.wikimedia.org/wikipedia/commons/2/2d/Picea_glauca_taiga.jpg"), "Koudgematigd klimaat met strenge winter");
            ResultaatBlad neeneeneejaneeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Gemengd-woudklimaat", "http://upload.wikimedia.org/wikipedia/commons/d/d6/Mixed_forest_near_Santa_Fe.jpg"), "Koelgematigd klimaat met koude winter");
            ResultaatBlad neeneeneejaneeneejaneejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Loofbosklimaat", "http://upload.wikimedia.org/wikipedia/commons/c/c6/Brussels_Zonienwoud.jpg"), "Koelgematigd klimaat met zachte winter");
            ResultaatBlad neeneeneejaneeneejaneeneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Subtropisch regenwoudklimaat", "http://upload.wikimedia.org/wikipedia/commons/d/d0/Aerial_view_of_the_Amazon_Rainforest.jpg"), "Warmgematigd altijd nat klimaat");
            ResultaatBlad neeneeneejaneeneeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Hardbladige-vegetatieklimaat van de centrale middelbreedten", "http://en.wikipedia.org/wiki/Sclerophyll#mediaviewer/File:Fynbos-landscape-1.jpg"), "Koelgematigd klimaat met natte winter");
            ResultaatBlad neeneeneejaneeneeneejaneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Hardbladige-vegetatieklimaat van de subtropen", "http://upload.wikimedia.org/wikipedia/commons/9/93/Garrigue_herault.jpg"), "Warmgematigd klimaat met natte winter");
            ResultaatBlad neeneeneejaneeneeneeneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Subtropisch savanneklimaat", "http://upload.wikimedia.org/wikipedia/commons/1/11/Savanna_towards_the_south-east_from_the_south-west_of_Taita_Hills_Game_Lodge_within_the_Taita_Hills_Wildlife_Sanctuary_in_Kenya.jpg"), "Warmgematigd klimaat met natte zomer");
            ResultaatBlad neeneeneeneeneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Tropisch savanneklimaat", "http://upload.wikimedia.org/wikipedia/commons/1/11/Savanna_towards_the_south-east_from_the_south-west_of_Taita_Hills_Game_Lodge_within_the_Taita_Hills_Wildlife_Sanctuary_in_Kenya.jpg"), "Warm klimaat met nat seizoen");
            ResultaatBlad neeneeneeneejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Tropisch regenwoudklimaat", "http://upload.wikimedia.org/wikipedia/commons/d/d0/Aerial_view_of_the_Amazon_Rainforest.jpg"), "Warm altijd nat klimaat");


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
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Tk"), VraagTekst = "Wat is de temperatuur van de koudste maand (Tk)?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("D"), VraagTekst = "Hoeveel droge maanden zijn er (D)?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Nz"), VraagTekst = "Hoeveelheid neerslag in de zomer?" });
                eersteGraad.Vragen.Add(new Vraag() { Parameter = _parameterFactory.MaakParameter("Nw"), VraagTekst = "Hoeveelheid neerslag in de winter?" });

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
                
                    
                int[] gemiddeldeNeerUkkel = {67,54,73,57,70,78,75,63,59,71,78,76 };
                double[] gemiddeldeTempUkkel = { 2.5,3.2,5.7,8.7,12.7,15.5,17.2,17.2,14.4,10.4,2.5,3.4};
                belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(gemiddeldeTempUkkel, gemiddeldeNeerUkkel).ToList(), 50.8, 4.35) { Locatie = "Ukkel", BeginJaar = 1961, EindJaar = 2009 });
                europa.VoegLandToe(belgie);

                belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { 2.2,2.2,5.3,8.8,12.3,15.6,17.0,17.0,14.3,10.1,6.0,3.2 },
                new int[] { 62,49,48,49,60,70,80,80,73,71,67,67 }).ToList(), 51.2, 4.45
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Antwerpen - Deurne" });

                belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(
               new double[] { -1.0,0.2,2.4,6.0,9.6,12.8,14.4,14.3,11.9,7.7,3.0,0.1 },
               new int[] { 90,74,73,69,77,84,93,94,89,92,91,95 }).ToList(), 50.033056, 5.4
           ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "St-Hubert" });

                belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(
             new double[] { -2.1,-1.2,1.0,4.8,8.8,12.0,13.6,13.4,10.9,6.8,2.0,-1.1 },
             new int[] { 140,112,103,91,98,103,106,101,96,110,124,142 }).ToList(), 50.466944, 6.182778
         ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Elsenborn" });

                belgie.VoegKlimatogramToe(new Klimatogram(VormMaanden(
   new double[] { 2.4,3.0,5.2,8.4,12.1,15.1,16.8,16.6,14.3,10.3,6.2,3.2 },
   new int[] { 51,42,46,50,59,65,72,74,72,72,64,59 }).ToList(), 50.966667, 3.816667
) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Gent - Melle" });

                Land frankrijk = new Land("Frankrijk");


                frankrijk.VoegKlimatogramToe(new Klimatogram(VormMaanden(
            new double[] { 8.6,9.0,10.1,12.3,15.7,19.1,21.9,22.1,19.9,16.7,12.6,9.6},
            new int[] { 74,70,58,52,40,19,11,20,44,87,96,76}).ToList(), 41.966944,8.8
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Ajaccio" });

                frankrijk.VoegKlimatogramToe(new Klimatogram(VormMaanden(
            new double[] { 5.9,7.1,8.8,11.3,14.6,17.8,20.2,19.9,18.0,14.0,9.1,6.4 },
            new int[] { 100,86,76,72,77,56,47,54,74,88,94,99}).ToList(), 40.8333333, 0.7
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Bordeaux" });
               
                frankrijk.VoegKlimatogramToe(new Klimatogram(VormMaanden(
            new double[] { 3.5,4.5,6.8,9.7,13.3,16.4,18.4,18.2,15.7,11.8,6.9,4.3},
            new int[] { 54,46,54,47,63,58,54,52,54,56,56,56 }).ToList(), 48.966667,2.45
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Parijs" });

                europa.Landen.Add(frankrijk);

                Land griekenland = new Land("Griekenland");
                griekenland.VoegKlimatogramToe(new Klimatogram(VormMaanden(
                new double[] { 10.0, 10.5, 12.4, 16.0, 20.6, 25.0, 27.8, 24.3, 19.3, 15.4, 12.0, 9.0 },
                new int[] { 45, 48, 44, 25, 14, 6, 6, 8, 10, 48, 51, 66 }).ToList(), 50.8, 4.35
            ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Athene" });

                griekenland.VoegKlimatogramToe(new Klimatogram(VormMaanden(
            new double[] { 5.0,6.7,9.6,14.2,19.5,24.2,26.5,25.8,21.8,16.1,10.9,6.7 },
            new int[] { 37,40,46,36,44,32,26,21,26,41,58,53 }).ToList(), 40.516667,22.966667
        ) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Thessaloniki" });

                griekenland.VoegKlimatogramToe(new Klimatogram(VormMaanden(
   new double[] { 11.7,12.0,16.6,16.7,20.5,24.7,26.9,26.9,24.6,20.6,16.4,13.4 },
   new int[] { 148,118,75,24,14,3,0,0,7,64,88,145 }).ToList(), 36.4, 28.066667
) { BeginJaar = 1961, EindJaar = 1990, Locatie = "Rhodos" });

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