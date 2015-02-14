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
        public Graad Graad { get; set; }

        [Range(1,2, ErrorMessage = "Graad moet tussen 1 en 2 liggen.")]
        public int? Jaar { get; set; }


        public LeerlingIndexViewModel() {
            
        }
        
        public LeerlingIndexViewModel(Leerling l) {
            Graad = l.Graad;
            Jaar = l.Jaar;
          
        }
    }
}