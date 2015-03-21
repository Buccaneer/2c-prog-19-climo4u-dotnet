using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;
using Ninject;


namespace Klimatogrammen.Infrastructure
{
    public class LeerlingModelBinder:IModelBinder {
   
        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return controllerContext.HttpContext.Session["leerling"] as Leerling;
        }
    }
}