using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace Zo.Xapp
{
    [DependsOn(
        typeof(XappDomainSharedModule)
        )]
    public class XappDomainModule : AbpModule
    {
    }
}
