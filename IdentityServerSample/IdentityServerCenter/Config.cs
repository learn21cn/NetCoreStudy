using IdentityServer4.Models;
using System.Collections;
using System.Collections.Generic;
using IdentityServer4.Test;


namespace IdentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","My Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //这里是客户端模式
                new Client()
                {                    
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes = {"api"}

                },
                
                //这里是密码模式
                new Client()
                {                    
                    ClientId="pwdClient",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes = {"api"},
                    //这样设置在访问时不再需要上面的ClientSecrets
                    RequireClientSecret=false

                }
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
                    Username="Test",
                    Password="123456"

                }
                
            };
        }

    }
}
