using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Infrastructure
{
    public class VraagRepositoryModelBinder:IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            //Leerling leerling = controllerContext.HttpContext.Session["leerling"] as Leerling;
            //if (controllerContext.HttpContext.Session["vraagRepository"] == null && leerling != null)
            //{
            //    return VraagRepository.CreerVragenVoorKlimatogram(leerling.Klimatogram);
            //}
            return controllerContext.HttpContext.Session["vraagRepository"] as VraagRepository;
        }
    }
}