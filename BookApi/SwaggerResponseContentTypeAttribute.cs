using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace BookApi
{
    /// <summary>
    /// SwaggerResponseContentTypeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SwaggerResponseContentTypeAttribute : Attribute
    {
        /// <summary>
        /// SwaggerResponseContentTypeAttribute
        /// </summary>
        /// <param name="responseType"></param>
        public SwaggerResponseContentTypeAttribute(string responseType)
        {
            ResponseType = responseType;
        }
        /// <summary>
        /// Response Content Type
        /// </summary>
        public string ResponseType { get; private set; }

        /// <summary>
        /// Remove all other Response Content Types
        /// </summary>
        public bool Exclusive { get; set; }
    }

    /// <summary>
    /// Swagger related class to allow adding custom content type to action methods. like application/json-patch+json
    /// </summary>
    /// <seealso cref="Swashbuckle.Swagger.IOperationFilter" />
    public class ResponseContentTypeOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="schemaRegistry">The schema registry.</param>
        /// <param name="apiDescription">The API description.</param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var requestAttributes = apiDescription.GetControllerAndActionAttributes<SwaggerResponseContentTypeAttribute>().FirstOrDefault();

            if (requestAttributes != null)
            {
                if (requestAttributes.Exclusive)
                    operation.produces.Clear();

                operation.produces.Add(requestAttributes.ResponseType);
            }
        }
    }
}