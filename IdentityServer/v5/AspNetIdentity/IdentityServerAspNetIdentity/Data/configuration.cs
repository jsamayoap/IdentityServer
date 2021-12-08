using System.Collections.Generic;
using Duende.IdentityServer.Models;

namespace anubis.Data
{
    public static class configuration
    {
        public static IEnumerable<ApiScope> getApiScopes() => new List<ApiScope>
            {
                new ApiScope("osiris", "Backend DRCPFA")
                {
                    UserClaims=new List<string> { "roles" }
                }
            };

        public static List<ApiResource> apiResources = new List<ApiResource>
        {
            new ApiResource("osiris", "My API #1")
        };
    }
}
