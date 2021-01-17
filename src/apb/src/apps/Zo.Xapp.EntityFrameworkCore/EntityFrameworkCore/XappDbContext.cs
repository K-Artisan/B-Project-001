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
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See ShootingRangeMigrationsDbContext for migrations.
     */
    [ConnectionStringName(XappConsts.ConnectionStringName)]
    public class XappDbContext : AbpDbContext<XappDbContext>
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside ShootingRangeDbContextModelCreatingExtensions.ConfigureShootingRange
         */
        //public DbSet<AppUser> Users { get; set; }

        public XappDbContext(DbContextOptions<XappDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            //builder.Entity<AppUser>(b =>
            //{
            //    b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

            //    b.ConfigureByConvention();
            //    b.ConfigureAbpUser();

            //    /* Configure mappings for your additional properties
            //     * Also see the ShootingRangeEfCoreEntityExtensionMappings class
            //     */
            //});

            /* Configure your own tables/entities inside the ConfigureShootingRange method */

            builder.ConfigureXapp();
        }
    }
}
