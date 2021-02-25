using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using GasMe.Api;
using GasMe.Api.Contracts.V1;


namespace GasMe.IntegrationTest
{
    public class UnitTest1
    {
        private readonly HttpClient _client;
        
        public UnitTest1(){
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            var response = await _client.GetAsync(OrderRoute.Gets);
        }
    }
}
 