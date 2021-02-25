// using System;
// using Xunit;
// using Microsoft.AspNetCore.Mvc.Testing;
// using System.Net;
// using System.Threading.Tasks;
// using GasMe.Service.Extentions;
// using GasMe.Api.Contracts.V1;
// using FluentAssertions;
// using System.Collections.Generic;
// using GasMe.Data.Models;
// using GasMe.Data.Models.EntityBase;


// namespace GasMe.IntegrationTest.ControllerTest.Sqlite
// {
//     public class OrderControllerTest : OrderControllerTestBase
//     {
//         public OrderControllerTest() : base( new DbContextOptionsBuilder<ItemsContext>().UseSqlite("Filename=Test.db").Options)
//         {
//         }

//         [Fact]
//         public async Task Gets_WithoutAnyOrders_ReturnsEmptyResponse(){
//             /*--------
//             * ARRANGE
//             *--------*/
//             await AuthenticateAsync();

//             /*----
//             * ACT
//             *----*/
//             var response = await _client.GetAsync(OrderRoute.Gets);
//             //var response = await _client.GetAsync(InboxRoute.Gets);
//             /*--------
//             * ASSERT
//             *--------*/
//             response.StatusCode.Should().Be(HttpStatusCode.OK);
//             var responseContent = (await response.Content.ReadAsAsync<ResultBase<List<Order>>>()).data;
//             (responseContent).Should().NotBeNullOrEmpty();
//         }
//     }
// }
 