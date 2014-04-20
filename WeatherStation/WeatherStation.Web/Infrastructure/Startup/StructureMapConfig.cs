using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeatherStation.Web.Infrastructure.DependencyResolution;
using WeatherStation.Web.Services.OpenWeatherMap;

namespace WeatherStation.Web.Infrastructure.Startup
{
    public class StructureMapConfig : IStartupTask
    {
        public readonly IContainer Container;

        public StructureMapConfig(IContainer container)
        {
            this.Container = container;
        }

        public void Execute()
        {
            Container.Configure(x =>
            {
                x.For<IContainer>().Use(Container);
                x.For<GlobalFilterCollection>().Use(GlobalFilters.Filters);
                x.For<RouteCollection>().Use(RouteTable.Routes);
                x.For<BundleCollection>().Use(BundleTable.Bundles);
                
                x.Scan(scan =>
                {
                    //scan.TheCallingAssembly();
                    scan.AssemblyContainingType<StructureMapConfig>();
                    scan.AddAllTypesOf<IStartupTask>();
                });

                //both the weather map service and settings do not store state and are thread safe, 
                //so we can just share a single opbject
                x.For<IOpenWeatherMapService>().Use(new OpenWeatherMapService());
                x.For<ISettings>().Use(new SettingsFromConfig());

            });

           

            //hook up the container to MVC and Web API
            DependencyResolver.SetResolver(new SmDependencyResolver(Container));
        }

        public int ExecutionOrder
        {
            get { return -1; }
        }
    }
}