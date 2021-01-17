using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
//using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Zo.Xapp.EntityFrameworkCore
{
    [DependsOn(
        typeof(XappDomainModule),
        //typeof(AbpEntityFrameworkCoreSqlServerModule) //MS SqlServer 数据库
        typeof(AbpEntityFrameworkCoreMySQLModule) // MySQL 数据库
        )]
    public class XappEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context) 
        {
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<XappDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also XappMigrationsDbContextFactory for EF Core tooling. */
                //options.UseSqlServer();
                options.UseMySQL();
            });
        }
    }
}
