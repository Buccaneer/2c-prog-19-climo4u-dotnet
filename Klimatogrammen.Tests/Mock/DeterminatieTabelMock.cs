using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Tests.Mock
{
   public class DeterminatieTabelMock
    {
       /// <summary>
       /// Maakt de determinatietabel aan
       /// Zie DeterminatieTabel mock afbeelding ter referentie en verduidelijking
       /// </summary>
       /// <returns>Retourneert een determinatietabel</returns>

       public DeterminatieTabel MaakDeterminatieTabel()
       {
           //Determinatietabel aanmaken
           DeterminatieTabel tabel = new DeterminatieTabel();

           //Factory aanmaken voor parameters
           ParameterFactory parameterFactory = new ParameterFactory();

           //Nieuwe vergelijking aanmaken (Tw < 10°C)
           Vergelijking vergelijking = new Vergelijking();
           vergelijking.LinkerParameter = parameterFactory.MaakParameter("Tw");
           vergelijking.RechterParameter = parameterFactory.MaakConstanteParameter(10);
           vergelijking.Operator = Operator.KleinerDan;

           //Ja knoop aanmaken van de eerste vergelijking
           //Vergelijking aanmaken van de ja knoop (Tw < 0°C)
           Vergelijking vergelijking2 = new Vergelijking();
           vergelijking2.LinkerParameter = parameterFactory.MaakParameter("Tw");
           vergelijking2.RechterParameter = parameterFactory.MaakConstanteParameter(0);
           vergelijking2.Operator = Operator.KleinerDan;

           //Resultaatknoop voor ja en nee Tw < 0°C
           DeterminatieKnoop resultaatKnoopJa = new ResultaatBlad("","Koud zonder dooiseizoen");
           DeterminatieKnoop resultaatKnoopNee = new ResultaatBlad("","Koud met dooiseizoen");

           //Ja knoop instellen van de ja tak van de eerste vergelijking
           DeterminatieKnoop jaKnoop = new BeslissingsKnoop(vergelijking2, resultaatKnoopJa, resultaatKnoopNee);

           //Nee knoop aanmaken van de beginknoop
           Vergelijking vergelijking3 = new Vergelijking();
           vergelijking3.LinkerParameter = parameterFactory.MaakParameter("Tw4");
           vergelijking3.RechterParameter = parameterFactory.MaakConstanteParameter(10);
           vergelijking3.Operator = Operator.KleinerDan;

           //Resultaatknoop aanmaken van vergelijking 3
           DeterminatieKnoop resultaatKnoopJa2 = new ResultaatBlad("","Koud gematigd");

           //Nee knoop aanmaken van vergelijking 3
           Vergelijking vergelijking4 = new Vergelijking();
           vergelijking4.LinkerParameter = parameterFactory.MaakParameter("Tk");
           vergelijking4.RechterParameter = parameterFactory.MaakConstanteParameter(18);
           vergelijking4.Operator = Operator.KleinerDan;

           //Resultaat knoop nee aanmaken van vergelijking 4
           DeterminatieKnoop resultaatKnoopNee2 = new ResultaatBlad("", "Warm");

           //Vergelijking aanmaken van ja knoop 2
           Vergelijking vergelijking5 = new Vergelijking();
           vergelijking5.LinkerParameter = parameterFactory.MaakParameter("Nj");
           vergelijking5.RechterParameter = parameterFactory.MaakConstanteParameter(400);
           vergelijking5.Operator = Operator.GroterDan;

           //Resultaat knoop nee aanmaken van vergelijking 5
           DeterminatieKnoop resultaatKnoopNee4 = new ResultaatBlad("","Gematigd en droog"); 

           //Vergelijking aanmaken van ja knoop 3
           Vergelijking vergelijking6 = new Vergelijking();
           vergelijking6.LinkerParameter = parameterFactory.MaakParameter("Tk");
           vergelijking6.RechterParameter = parameterFactory.MaakConstanteParameter(-3);
           vergelijking6.Operator = Operator.KleinerDan;

           //Resultaat knoop aanmaken van vergelijking 6
           DeterminatieKnoop resultaatKnoopJa3 = new ResultaatBlad("","Koel gematigd met strenge winter");

           //Vergelijking aanmaken van nee knoop 3
           Vergelijking vergelijking7 = new Vergelijking();
           vergelijking7.LinkerParameter = parameterFactory.MaakParameter("Tw");
           vergelijking7.RechterParameter = parameterFactory.MaakConstanteParameter(22);
           vergelijking7.Operator=Operator.KleinerDan;

           //Resultaat knoop aanmaken van vergelijking 7
           DeterminatieKnoop resultaatKnoopJa4 = new ResultaatBlad("","Koel gematigd met zachte winter");
           DeterminatieKnoop resultaatKnoopNee3 = new ResultaatBlad("","Warm gematigd met natte winter");

           //Nee knoop aanmaken van vergelijking 6
           DeterminatieKnoop neeKnoop3 = new BeslissingsKnoop(vergelijking7, resultaatKnoopJa4, resultaatKnoopNee3);

           //Ja knoop aanmaken van vergelijking 5
           DeterminatieKnoop jaKnoop3 = new BeslissingsKnoop(vergelijking6, resultaatKnoopJa3, neeKnoop3);

           //Ja knoop aanmaken van vergelijking 4
           DeterminatieKnoop jaKnoop2 = new BeslissingsKnoop(vergelijking5, jaKnoop3, resultaatKnoopNee4);

           //Knoop aanmaken van vergelijking 4
           DeterminatieKnoop neeKnoop2 = new BeslissingsKnoop(vergelijking4,jaKnoop2, resultaatKnoopNee2);

           //Knoop aanmaken van vergelijking 3
           DeterminatieKnoop neeKnoop= new BeslissingsKnoop(vergelijking3,resultaatKnoopJa2, neeKnoop2);


           tabel.BeginKnoop = new BeslissingsKnoop(vergelijking, jaKnoop,neeKnoop);

           return tabel;
       }
    }
}
