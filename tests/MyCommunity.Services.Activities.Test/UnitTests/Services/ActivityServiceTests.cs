using System.Threading.Tasks;
using Moq;
using Xunit;
using MyCommunity.Services.Activities.Domain.Repositories;
using MyCommunity.Services.Activities.Domain.Models;
using MyCommunity.Services.Activities.Services;
using System;

namespace MyCommunity.Services.Activities.Test.UnitTests.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task activity_service_add_async_should_succeed()
        {
            var category = "test";
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(x=>x.GetAsync(category)).ReturnsAsync(new Category(category));
            var activityService = new ActivityService(activityRepositoryMock.Object,categoryRepositoryMock.Object);
            var id = Guid.NewGuid();
            await activityService.AddAsync(id, Guid.NewGuid(), category, "Activity", It.IsAny<string>(), DateTime.UtcNow);
            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}