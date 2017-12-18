using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BookApi
{
    /// <summary>
    /// Startup class
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Startup method.
        /// </summary>
        protected void Application_Start()
        {
            BookApi.Data.AutoMapperConfiguration.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
