using System;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GasMe.Api;
using GasMe.Data;
using GasMe.Data.Models;
using System.Collections.Generic;
using GasMe.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using GasMe.Api.Contracts.V1;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using GasMe.Service.Extentions;
using GasMe.Data.Models.EntityBase;
using Microsoft.AspNetCore.TestHost;




namespace GasMe.IntegrationTest
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;
        
        protected IntegrationTest(){
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    var serviceDescriptorRemovied = descriptor != null ? services.Remove(descriptor) : false;

                    services.RemoveAll(typeof(ApplicationDbContext));
                    services.AddDbContext<ApplicationDbContext>(
                        options => {options.UseInMemoryDatabase(databaseName: "InMemoryDb");},
                        //options => {options.UseSqlite("Filename=Test.db");}, 
                        ServiceLifetime.Scoped, 
                        ServiceLifetime.Scoped);
                });
            });
            _client = appFactory.CreateClient();
        }
        

        protected async Task AuthenticateAsync(){
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync(){
            var request = await _client.PostAsJsonAsync(IdentityRoute.Auth, new User{
                status = EntityStatus.New,
                identityUser = new IdentityUser{
                    Email = "9ywq2a12siod3f@gmail.com",
                    PasswordHash = "Password-123",
                    UserName = "9yw12qaesjjdf"
                }
            });

            var response = (await request.Content.ReadAsAsync<ResultBase<User>>()).data;
            return response.token;
        }
    
        protected async Task<ResultBase<List<Order>>> SaveOrderAsync(Order request){
            var requestBatch = new List<Order>{ request };
            var response = await _client.PostAsJsonAsync(OrderRoute.Post, requestBatch);
            return (await response.Content.ReadAsAsync<ResultBase<List<Order>>>());
        }
    }
}
 