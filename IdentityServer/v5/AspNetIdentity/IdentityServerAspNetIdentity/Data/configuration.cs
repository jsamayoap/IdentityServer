using System.Collections.Generic;
using Duende.IdentityServer.Models;

namespace anubis.Data
{
    public static class configuration
    {
        public static IEnumerable<ApiScope> getApiScopes() => new List<ApiScope>
            {
                new ApiScope("apiPagos", "Backend DRCPFA")
                {
                    UserClaims=new List<string> { "roles" }
                }
            };

        public static IEnumerable<ApiResource> apiResources = new List<ApiResource>
        {
            new ApiResource("apiPagos", "API de Pagos", new List<string> { "roles" })
        };
    }
}
