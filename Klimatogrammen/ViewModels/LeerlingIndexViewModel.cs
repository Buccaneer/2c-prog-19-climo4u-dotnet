using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class LeerlingIndexViewModel
    {
        [Required(ErrorMessage = "Graad moet worden meegegeven.")]
        public GraadKiezen Graad { get; set; }

        [Range(1,2, ErrorMessage = "Jaar moet 1 of 2 zijn.")]
        public int? Jaar { get; set; }


        public LeerlingIndexViewModel() {
            
        }
        
        public LeerlingIndexViewModel(Leerling l) {
            Graad = (GraadKiezen) l.Graad.Nummer;
            Jaar = l.Graad.Jaar;
          
        }
    }

    public enum GraadKiezen : int{
        Eerste = 1,
        Tweede = 2,
        Derde = 3
    }
}