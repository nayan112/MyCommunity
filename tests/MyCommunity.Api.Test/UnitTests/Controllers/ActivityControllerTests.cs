using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyCommunity.Api.Controllers;
using MyCommunity.Api.Repositories;
using MyCommunity.Common.Commands;
using RawRabbit;
using Xunit;

namespace MyCommunity.Api.Test.UnitTests.Controllers
{
    public class ActivityControllerTest
    {
        [Fact]
        public async Task activities_controller_post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var activityRepositoryMock = new Mock<IActivityRepository>(); 
            var controller = new ActivitiesController(busClientMock.Object,activityRepositoryMock.Object);
            var userId = Guid.NewGuid();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext=new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name,userId.ToString())},"test"
                    ))
                }
            };
            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId= userId
            };
            var result = await controller.Post(command);
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            //contentResult.Location.ShouldBeEquivalentTo($"Activities/{command.Id}");

        }
    }
}