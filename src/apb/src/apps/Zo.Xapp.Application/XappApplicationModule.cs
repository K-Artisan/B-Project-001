using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Zo.Xapp
{
    [DependsOn(
        typeof(XappDomainModule),
        typeof(XappApplicationContractsModule),
        typeof(AbpLocalizationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class XappApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                //通过扫描程序集的方式搜索`Porfile`类，并添加到AutoMapper配置中
                options.AddMaps<XappApplicationModule>();
            });
        }
    }
}
