using System.Collections.Generic;
using Duende.IdentityServer.Models;

namespace anubis.Data
{
    public static class configuration
    {
        public static IEnumerable<ApiScope> apiScopes => new List<ApiScope>
        {
            new ApiScope("apiPagos", "Backend DRCPFA")
            {
                UserClaims=new List<string> { "roles" }
            },
            new ApiScope("apiBanca", "Backend DRCPFA")
            {
                UserClaims=new List<string> { "roles" }
            }
        };

        public static IEnumerable<ApiResource> apiResources = new List<ApiResource>
        {
            new ApiResource("apiPagos", "API de Pagos", new List<string> { "roles" }),
            new ApiResource("apiBanca", "API de Bancos", new List<string> { "roles" })
        };

        public static IEnumerable<Client> clients => new Client[] {
            new Client
            {
                ClientId = "bantrabAPI",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("JGusY=*w};&4A>D:".Sha256())
                },
                AllowedScopes = { "apiBanca" }
            },
            new Client
            {
                ClientId = "seth",
                ClientName="DRCPFA FrontEnd",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { "http://localhost:4200/auth-callback", "http://localhost:4200/silent-refresh.html" },
                PostLogoutRedirectUris = { "http://localhost:4200/" },
                AllowedCorsOrigins =  { "http://localhost:4200" },
                AllowedScopes = { "openid", "profile", "email", "phone", "apiPagos" }
            },
            new Client {
                ClientId = "osiris",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("Secreto@3#2!2018".Sha256())
                },
                AllowedScopes = { "apiPagos", "apiBanca" }
            }
        };
    }
}
