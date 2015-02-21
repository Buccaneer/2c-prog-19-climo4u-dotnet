using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Klimatogrammen.Infrastructure;
using Klimatogrammen.Models.DAL;
using Klimatogrammen.Models.Domein;

namespace Klimatogrammen
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            KlimatogrammenContext db = new KlimatogrammenContext();
            ModelBinders.Binders.Add(typeof(Leerling), new LeerlingModelBinder());
            ModelBinders.Binders.Add(typeof(Continent), new ContinentModelBinder());
            ModelBinders.Binders.Add(typeof(Land), new LandModelBinder());
            db.Database.Initialize(true); 
        }
    }
}
