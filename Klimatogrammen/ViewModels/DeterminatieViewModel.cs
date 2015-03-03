using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class DeterminatieIndexViewModel
    {
        public string KlimaatType { get; set; }
        public string VegetatieType { get; set; }
        public DeterminatieIndexViewModel()
        {
            
        }

        public DeterminatieIndexViewModel(ResultaatKnoop knoop)
        {
            KlimaatType = knoop.KlimaatType;
            VegetatieType = knoop.VegetatieType;
        }
    }
}