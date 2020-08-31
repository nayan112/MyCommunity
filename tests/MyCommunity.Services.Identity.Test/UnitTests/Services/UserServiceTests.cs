using FluentAssertions;
using Moq;
using MyCommunity.Common.Auth;
using MyCommunity.Services.Identity.Domain.Models;
using MyCommunity.Services.Identity.Domain.Repositories;
using MyCommunity.Services.Identity.Domain.Services;
using MyCommunity.Services.Identity.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyCommunity.Services.Identity.Test.UnitTests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task user_service_login_should_return_jwt() {
            var email = "test@test.com";
            var password = "password";
            var name = "name";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            encrypterMock.Setup(x => x.GetSalt()).Returns(salt);
            encrypterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);
            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken
            {
                Token = token
            });
            var user = new User(email,token);
            user.SetPassword(password, encrypterMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);
            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, jwtHandlerMock.Object);
            var jwt = await userService.LoginAsync(email, password);
            userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);
            jwtHandlerMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.Should().BeEquivalentTo(token);
        }
    }
}
