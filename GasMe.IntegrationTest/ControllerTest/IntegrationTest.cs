// using System;
// using Xunit;
// using Microsoft.AspNetCore.Mvc.Testing;
// using System.Net.Http;
// using System.Net.Http.Json;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;
// using GasMe.Api;
// using GasMe.Data;
// using GasMe.Data.Models;
// using GasMe.Data.Enums;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection.Extensions;
// using Microsoft.Extensions.DependencyInjection;
// using GasMe.Api.Contracts.V1;
// using Microsoft.AspNetCore.Identity;
// using System.Text.Json;
// using GasMe.Service.Extentions;
// using GasMe.Data.Models.EntityBase;
// using Microsoft.AspNetCore.TestHost;




// namespace GasMe.IntegrationTest.ControllerTest
// {
//     public class IntegrationTest
//     {
//         protected readonly HttpClient _client;
        
//         protected IntegrationTest(){
//             var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder => {
//                 builder.ConfigureTestServices(services => {
//                     services.RemoveAll(typeof(ApplicationDbContext));
//                     services.AddDbContext<ApplicationDbContext>(
//                         //options => {options.UseInMemoryDatabase(databaseName: "InMemoryDb");},
//                         options => {options.UseSqlite("Filename=Test.db");}, 
//                         ServiceLifetime.Scoped, 
//                         ServiceLifetime.Scoped);
//                 });
//             });
//             _client = appFactory.CreateClient();
//         }
        

//         protected async Task AuthenticateAsync(){
//             _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
//         }

//         private async Task<string> GetJwtAsync(){
//             var request = await _client.PostAsJsonAsync(IdentityRoute.Auth, new User{
//                 status = EntityStatus.New,
//                 identityUser = new IdentityUser{
//                     Email = "ywq2a12siod3f@gmail.com",
//                     PasswordHash = "Password-123",
//                     UserName = "yw12qaesjjdf"
//                 }
//             });

//             var response = (await request.Content.ReadAsAsync<ResultBase<User>>()).data;
//             return response.token;
//         }
//     }
// }
 