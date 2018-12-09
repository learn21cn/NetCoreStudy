using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.Models.IdentityResources;

namespace ConsentServer
{
    public class Config
    {
        //可以访问的Resource
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource( "api1","API Applacation")
            };
        }

        //允许的客户端
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId="mvc",
                    ClientName="Mvc Client",
                    ClientUri="http://localhost:5001",
                    LogoUri="https://chocolatey.org/content/packageimages/dotnetcore-vs.2.0.3-Preview.png",
                    AllowRememberConsent=true,
                    //AllowedGrantTypes=GrantTypes.Implicit,
                    AllowedGrantTypes=GrantTypes.HybridAndClientCredentials,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowOfflineAccess=true,
                    AllowAccessTokensViaBrowser=true,

                    //是否需要用户点击登录
                    RequireConsent =true,
                    //跳转登录到的客户端的地址
                    RedirectUris={ "http://localhost:5001/signin-oidc"},
                    //跳转登出到的客户端的地址
                    PostLogoutRedirectUris={ "http://localhost:5001/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken=true,

                    //运行访问的范围
                    AllowedScopes=
                    {
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api1"
                    }  
                    
                }
            };
        }

     

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new OpenId(),
                new Profile(),
                new Email()

            };
        }

        //测试用户
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="test@yahoo.com",

                    Password="password"

                }
            };
        }
    }
}
