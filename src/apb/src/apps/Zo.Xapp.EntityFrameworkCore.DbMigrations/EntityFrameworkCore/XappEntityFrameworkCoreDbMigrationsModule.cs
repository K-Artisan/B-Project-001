using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Zo.Xapp.EntityFrameworkCore
{
    [DependsOn(
        typeof(XappEntityFrameworkCoreModule)
    )]
    public class XappEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<XappMigrationsDbContext>();
        }
    }
}
