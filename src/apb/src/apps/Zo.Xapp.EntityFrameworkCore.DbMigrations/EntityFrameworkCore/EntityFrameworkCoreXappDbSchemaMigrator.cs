using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Zo.Xapp.Data;

namespace Zo.Xapp.EntityFrameworkCore
{
    public class EntityFrameworkCoreXappDbSchemaMigrator : IXappDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreXappDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ShootingRangeMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<XappMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
