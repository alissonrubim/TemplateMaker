using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace SuperNiceProject.Host.Lambda.Tests.Helpers
{
    public static class RequestHelper
    {
        public static APIGatewayProxyRequest CreateRequest(string HttpMethod, string Path, Dictionary<string, string> PathParams = null, object Body = null)
        {
            var request = new APIGatewayProxyRequest();
            request.HttpMethod = HttpMethod;
            request.Path = Path;
            if (Body != null)
                request.Body = JsonConvert.SerializeObject(Body);
            if (PathParams != null)
                request.PathParameters = PathParams;
            return request;
        }
    }
}
