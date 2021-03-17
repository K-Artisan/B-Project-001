using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Zo.Xapp.Users
{
    public class UserManager : DomainService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(
            [NotNull] string userName,
            [NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(userName, nameof(userName));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingUser = await _userRepository.FindByUserNameAsync(userName);
            if (existingUser != null)
            {
                throw new UserFriendlyException(userName);
            }

            return new User(GuidGenerator.Create(), userName, name);
        }
    }
}
