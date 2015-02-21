using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.Models.Domein;


namespace Klimatogrammen.Infrastructure
{
    public class ContinentModelBinder: IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return controllerContext.HttpContext.Session["continent"] as Continent;
        }
    }
}