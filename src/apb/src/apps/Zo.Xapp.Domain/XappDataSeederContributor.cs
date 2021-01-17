using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Zo.Xapp
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class XappDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        public XappDataSeederContributor()
        {
        }

        public Task SeedAsync(DataSeedContext context)
        {
 
            return Task.CompletedTask;
        }
    }
}
