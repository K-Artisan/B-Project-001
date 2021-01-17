using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Zo.Xapp.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See ShootingRangeDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     * 
     * 这个DbContext只用于数据库迁移。
     * 它不在运行时使用。有关运行时DbContext，请参阅BookStoreDbContext。
     * 它是一个统一配置所有使用的模块和您的应用程序的模型
     */

    [ConnectionStringName(XappConsts.ConnectionStringName)]
    public class XappMigrationsDbContext : AbpDbContext<XappMigrationsDbContext>
    {
        public XappMigrationsDbContext(DbContextOptions<XappMigrationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */
            //builder.ConfigureAuditLogging();
    

            /* Configure your own tables/entities inside the ConfigureShootingRange method */

            builder.ConfigureXapp();
        }
    }
}
