using Zo.Xapp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Zo.Xapp
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(XappEntityFrameworkCoreDbMigrationsModule),
        typeof(XappApplicationContractsModule)
        )]
    public class XappDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
