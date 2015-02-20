using System.Web.Mvc;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen.Infrastructure
{
    public class LandModelBinder:IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return controllerContext.HttpContext.Session["land"] as Land;
        }
    }
}