using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.ViewModels
{
    public class LeerlingIndexViewModel
    {
        public const string jaarErrorMessage = "Jaar moet worden meegegeven bij de tweede graad.";
        public const string graadErrorMessage = "Graad moet worden meegegeven.";
        private JaarKiezen? _jaar;

        [RequiredIf("Graad", GraadKiezen.Tweede, ErrorMessage = jaarErrorMessage)]
        [EnumDataType(typeof(JaarKiezen), ErrorMessage = jaarErrorMessage)]
        public JaarKiezen? Jaar {
            get
            {
                return _jaar; 
            }
            set {
                _jaar = Graad == GraadKiezen.Tweede ? value : null;
            }
        }

        [Required(ErrorMessage = graadErrorMessage)]
        [EnumDataType(typeof(GraadKiezen), ErrorMessage = graadErrorMessage)]
        public GraadKiezen Graad { get; set; }

        public LeerlingIndexViewModel()
        {
        }

        public LeerlingIndexViewModel(Leerling l)
        {
            Graad = (GraadKiezen)l.Graad.Nummer;
            Jaar = (JaarKiezen)l.Graad.Jaar;
        }
    }

    public enum GraadKiezen : int
    {
        Eerste = 1,
        Tweede = 2,
        Derde = 3
    }

    public enum JaarKiezen : int
    {
        Eerste = 1,
        Tweede = 2
    }
}