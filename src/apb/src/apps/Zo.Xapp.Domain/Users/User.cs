using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Zo.Xapp.Users
{
    public class User : FullAuditedAggregateRoot<Guid>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //public virtual List<UserFingerprint> Fingerprints { get; set; }

    }
}
