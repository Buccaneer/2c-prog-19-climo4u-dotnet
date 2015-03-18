using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels {
    public class OefeningLocatieVegTypesIndexViewModel {
        public bool? AllesJuist { get; set; }

        public ICollection<VegetatieVraagViewModel> Vragen { get; set; }
        public AntwoordViewModel Antwoorden { get; set; }

        public OefeningLocatieVegTypesIndexViewModel() {
        }

        public OefeningLocatieVegTypesIndexViewModel(Leerling leerling) {
            AllesJuist = null;
            Vragen =
                leerling.GeefKlimatogrammenDerdeGraad()
                    .Select(
                        k =>
                            new VegetatieVraagViewModel(leerling,
                                leerling.Graad.DeterminatieTabel.Determineer(k).VegetatieType.Foto)).ToList(); 
         
        }

    }
}