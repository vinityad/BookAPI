using Autofac;
using Autofac.Integration.WebApi;
using JsonPatch.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace BookApi
{
    /// <summary>
    /// API configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Add(new JsonPatchFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Configure AutoFac
            ConfigureAutofac(config);
        }


        private static void ConfigureAutofac(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Data.AppDataContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<Data.Infrastracture.UnitOfWork>().As<Data.Infrastracture.IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(Data.AppDataContext).Assembly).Where(o => o.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            IContainer container = builder.Build();

            // Assign Dependency resolver to MVC
            config.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);
        }
    }
}
