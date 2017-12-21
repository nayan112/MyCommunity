using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyCommunity.Api.Controllers;
using Xunit;

namespace MyCommunity.Api.Test.UnitTests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void home_controller_get_returns_string() {
            var controller = new HomeController();
            var result = controller.Get();
            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            contentResult.Content.ShouldBeEquivalentTo("Hello from MyCommunity API");
        }
    }
}
