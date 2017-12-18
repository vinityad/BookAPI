using System.Web.Http;
using WebActivatorEx;
using BookApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace BookApi
{
    /// <summary>
    /// Swagger configuration file
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Registers this instance.
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "BookApi");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                        c.OperationFilter<ResponseContentTypeOperationFilter>();
                    })
                .EnableSwaggerUi(c => { });
        }

        /// <summary>
        /// Gets the XML comments path.
        /// </summary>
        /// <returns>XML file path</returns>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\BookApi.XML",
                System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
