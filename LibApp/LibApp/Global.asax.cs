using LibApp.Controllers;
using LibApp.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace LibApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new Container(x => x.For<ILibraryService>().Use<LibraryService>());

            var library = container.GetInstance<HomeController>();

            // Create the depenedency resolver.
            //var resolver = new AutofacWebApiDependencyResolver(container);

            //// Configure Web API with the dependency resolver.
            //GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }
    }
}
