using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Zo.Xapp.Users
{
    public class User : FullAuditedAggregateRoot<Guid>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        //public virtual List<UserFingerprint> Fingerprints { get; set; }

        private User()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal User(
            Guid id,
            [NotNull] string userName,
            [NotNull] string name)
            : base(id)
        {
            SetUserName(userName);
            SetName(name);

        }

        private void SetUserName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: UserConsts.MaxUserNameLength
            );
        }

        internal User ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: UserConsts.MaxNameLength
            );
        }
    }
}
