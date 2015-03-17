using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels {
    public class OefeningLocatieVegTypesIndexViewModel {
        public ICollection<VegetatieVraagViewModel> Vragen { get; set; }
        public AntwoordViewModel Antwoorden { get; set; }

        public OefeningLocatieVegTypesIndexViewModel(Leerling leerling) {
            Vragen =
                leerling.GeefKlimatogrammenDerdeGraad()
                    .Select(
                        k =>
                            new VegetatieVraagViewModel(leerling,
                                leerling.Graad.DeterminatieTabel.Determineer(k).VegetatieType.Foto)).ToList(); // #LINQ
         
        }

    }
}