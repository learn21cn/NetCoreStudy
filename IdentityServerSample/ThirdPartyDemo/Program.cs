using System;
using IdentityModel.Client;
using System.Net.Http;

namespace ThirdPartyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var diso = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if(diso.IsError)
            {
                Console.WriteLine(diso.Error);
            }
            //这里要指定客户ID与secret
            var tokenClient=new TokenClient(diso.TokenEndpoint,"client","secret");
            //这里指定资源范围
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;
            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var response = httpClient.GetAsync("http://localhost:5000/api/values").Result;
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
            
        }
    }
}
