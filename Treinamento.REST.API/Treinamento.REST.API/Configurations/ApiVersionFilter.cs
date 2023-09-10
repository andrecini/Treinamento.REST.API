using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Treinamento.REST.API.Configurations
{
    internal class ApiVersionFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parametersToRemove = operation.Parameters.Where(x => x.Name == "api-version").ToList();
            foreach (var parameter in parametersToRemove)
                operation.Parameters.Remove(parameter);
        }
    }
}
