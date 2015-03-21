using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klimatogrammen.App_Start;
using Klimatogrammen.Models.Domein;
using Ninject;


namespace Klimatogrammen.Infrastructure
{
    public class ContinentModelBinder: IModelBinder
    {
        public ContinentModelBinder() {
        }


        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return controllerContext.HttpContext.Session["continent"] as Continent;
        }
    }
}