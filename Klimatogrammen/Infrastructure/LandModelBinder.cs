using System.Web.Mvc;
using System.Web.UI;
using Klimatogrammen.Models.Domein;
using Ninject;

namespace Klimatogrammen.Infrastructure
{
    public class LandModelBinder:IModelBinder {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return controllerContext.HttpContext.Session["land"] as Land;
        }
    }
}