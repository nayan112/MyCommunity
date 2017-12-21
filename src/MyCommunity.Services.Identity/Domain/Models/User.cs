using MyCommunity.Common.Exceptions;
using MyCommunity.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunity.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string Salt { get; protected set; }
        public string Password { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        protected User()
        {

        }
        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new MyCommunityExceptions("empty_user_email","user email cannot be empty");
            if (string.IsNullOrWhiteSpace(name))
                throw new MyCommunityExceptions("empty_user_name", "user name cannot be empty");
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }
        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new MyCommunityExceptions("empty_user_password", "user password cannot be empty");
            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }
        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.Equals(encrypter.GetHash(password, Salt));
    }
}
