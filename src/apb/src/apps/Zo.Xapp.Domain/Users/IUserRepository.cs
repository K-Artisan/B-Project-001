using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Zo.Xapp.Users
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> FindByUserNameAsync(string userName);
        Task<User> FindByNameAsync(string name);

        Task<List<User>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
