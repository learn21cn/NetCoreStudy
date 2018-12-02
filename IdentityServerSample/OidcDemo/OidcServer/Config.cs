using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace OidcServer
{
    public class Config
    {
       
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {                
                new Client()
                {
                    ClientId="mvc",
                    AllowedGrantTypes=GrantTypes.Implicit,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    
                    AllowedScopes={//可以访问的Resource
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                    },
                    RedirectUris={"http://localhost:5001/signin-oidc"},//跳转登录到的客户端的地址
                    PostLogoutRedirectUris={"http://localhost:5001/signout-callback-oidc"},//跳转登出到的客户端的地址
                    RequireConsent=false//是否需要用户点击确认进行跳转

                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","Api Application")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResources.Email()
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="Test",
                    Password="123456"

                }

            };
        }


    }
}
