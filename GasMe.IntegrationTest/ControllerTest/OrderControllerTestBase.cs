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


// namespace GasMe.IntegrationTest.ControllerTest
// {
//     public class OrderControllerTestBase
//     {
//         protected DbContextOptions<ItemsContext> _contextOptions { get; }

//         protected OrderControllerTestBase(DbContextOptions<ApplicationDbContest> contextOptions)
//         {
//             _contextOptions = contextOptions;

//             //Seed();
//         }

//         // private void Seed()
//         // {
//         //     using (var context = new ItemsContext(ContextOptions))
//         //     {
//         //         context.Database.EnsureDeleted();
//         //         context.Database.EnsureCreated();

//         //         var one = new Item("ItemOne");
//         //         one.AddTag("Tag11");
//         //         one.AddTag("Tag12");
//         //         one.AddTag("Tag13");

//         //         var two = new Item("ItemTwo");

//         //         var three = new Item("ItemThree");
//         //         three.AddTag("Tag31");
//         //         three.AddTag("Tag31");
//         //         three.AddTag("Tag31");
//         //         three.AddTag("Tag32");
//         //         three.AddTag("Tag32");

//         //         context.AddRange(one, two, three);

//         //         context.SaveChanges();
//         //     }
//         // }
//     }
// }
 