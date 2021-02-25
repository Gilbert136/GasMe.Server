using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using GasMe.Service.Extentions;
using GasMe.Api.Contracts.V1;
using FluentAssertions;
using System.Collections.Generic;
using GasMe.Data.Models;
using GasMe.Data.Models.EntityBase;
using System.Linq;



namespace GasMe.IntegrationTest
{
    public class OrderControllerTest: IntegrationTest
    {
        //[Fact]
        public async Task GetsAsync_WithoutAnyOrders_ReturnsEmptyResponse(){
            /*--------
            * ARRANGE
            *--------*/
            await AuthenticateAsync();

            /*----
            * ACT
            *----*/
            var response = await _client.GetAsync(OrderRoute.Gets);
            //var response = await _client.GetAsync(InboxRoute.Gets);
            /*--------
            * ASSERT
            *--------*/
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = (await response.Content.ReadAsAsync<ResultBase<List<Order>>>()).data;
            (responseContent).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsOrder_WhereOrderExistsInTheDatabase(){
            /*--------
            * ARRANGE
            *--------*/
            await AuthenticateAsync();
            var createdOrder = (await SaveOrderAsync(new Order{
                label = "123",
                transactionStatus = Data.Enums.TransactionStatus.Pending,
                status = Data.Enums.EntityStatus.New
            })).data;

            /*----
            * ACT
            *----*/
            var response = await _client.GetAsync(OrderRoute.Get.Replace("{id}", createdOrder.FirstOrDefault().id.ToString()));
        
            /*--------
            * ASSERT
            *--------*/
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedOrder = (await response.Content.ReadAsAsync<ResultBase<Order>>()).data;
            returnedOrder.id.Should().Be(createdOrder.FirstOrDefault().id);
        }
    }
}
 