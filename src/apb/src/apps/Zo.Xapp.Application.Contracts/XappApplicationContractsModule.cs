using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Zo.Xapp
{
    [DependsOn(
        typeof(XappDomainSharedModule)
    )]
    public class XappApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            XappDtoExtensions.Configure();
        }
    }
}
