using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Tests.Mock
{
    class GroteDeterminatieTabelMock
    {

        public DeterminatieTabel MaakDeterminatieTabel()
        {
                     DeterminatieTabel dt = new DeterminatieTabel();
            ParameterFactory _parameterFactory = new ParameterFactory();

            ResultaatBlad jajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Ijswoestijnklimaat",""),"Koud klimaat zonder dooiseizoen");
            ResultaatBlad janeeResultaatKnoop = new ResultaatBlad(new VegetatieType("Ijswoestijnklimaat","Toendraklimaat"),"Koud klimaat met dooiseizoen");
            ResultaatBlad neejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Taigaklimaat",""),"Koudgematigd klimaat met strenge winter");
            ResultaatBlad neeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Woestijnklimaat van de middelbreedten",""),"Gematigd altijd droog klimaat");
            ResultaatBlad neeneejaneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Woestijnklimaat van de tropen",""),"Warm altijd droog klimaat");
            ResultaatBlad neeneeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Steppeklimaat",""),"Gematigd, droog klimaat");
            ResultaatBlad neeneeneejaneejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Taigaklimaat",""),"Koudgematigd klimaat met strenge winter");
            ResultaatBlad neeneeneejaneeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Gemengd-woudklimaat",""),"Koelgematigd klimaat met koude winter");
            ResultaatBlad neeneeneejaneeneejaneejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Loofbosklimaat",""),"Koelgematigd klimaat met zachte winter");
            ResultaatBlad neeneeneejaneeneejaneeneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Subtropisch regenwoudklimaat",""),"Warmgematigd altijd nat klimaat");
            ResultaatBlad neeneeneejaneeneeneejajaResultaatKnoop = new ResultaatBlad(new VegetatieType("Hardbladige-vegetatieklimaat van de centrale middelbreedten",""),"Koelgematigd klimaat met natte winter");
            ResultaatBlad neeneeneejaneeneeneejaneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Hardbladige-vegetatieklimaat van de subtropen",""),"Warmgematigd klimaat met natte winter");
            ResultaatBlad neeneeneejaneeneeneeneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Subtropisch savanneklimaat",""),"Warmgematigd klimaat met natte zomer");
            ResultaatBlad neeneeneeneeneeResultaatKnoop = new ResultaatBlad(new VegetatieType("Tropisch savanneklimaat",""), "Warm klimaat met nat seizoen");
            ResultaatBlad neeneeneeneejaResultaatKnoop = new ResultaatBlad(new VegetatieType("Tropisch regenwoudklimaat",""),"Warm altijd nat klimaat");


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
        
    }
}
