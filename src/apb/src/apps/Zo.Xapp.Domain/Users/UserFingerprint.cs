using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Zo.Xapp.Users
{
    public class UserFingerprint : FullAuditedEntity<Guid>
    {
        public Guid UserId { get; set; }
        /// <summary>
        /// 指纹
        /// </summary>
        public string Fingerprint { get; set; }
        
    }
}
