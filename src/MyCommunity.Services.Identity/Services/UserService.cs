using MyCommunity.Common.Auth;
using MyCommunity.Common.Exceptions;
using MyCommunity.Services.Identity.Domain.Models;
using MyCommunity.Services.Identity.Domain.Repositories;
using MyCommunity.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunity.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository,IEncrypter encrypter,IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }
        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                throw new MyCommunityExceptions("invalid_credentials", $"Email:{email} is invalid!");
            if(!user.ValidatePassword(password, _encrypter))
                throw new MyCommunityExceptions("invalid_credentials", $"invalid credentials!");
            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new MyCommunityExceptions("email_already_used", $"Email:{email} is already in use!");
            var newUser = new User(email, name);
            newUser.SetPassword(password, _encrypter);
            await _userRepository.AddAsync(newUser);
        }
    }
}
