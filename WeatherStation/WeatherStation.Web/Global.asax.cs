using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeatherStation.Web.Infrastructure.Startup;

namespace WeatherStation.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //create the IoC container and run all the startup tasks
            var container = ObjectFactory.Container;
            new StructureMapConfig(container).Execute();


            foreach (var startup in container.GetAllInstances<IStartupTask>()
                .Where(t => t.GetType() != typeof(StructureMapConfig))
                .OrderBy(x => x.ExecutionOrder))
                startup.Execute();
        }
    }
}
