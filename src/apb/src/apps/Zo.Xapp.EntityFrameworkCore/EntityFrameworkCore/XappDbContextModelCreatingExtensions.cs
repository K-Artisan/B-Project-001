using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zo.Xapp.Users;

namespace Zo.Xapp.EntityFrameworkCore
{
    public static class XappDbContextModelCreatingExtensions
    {
        public static void ConfigureXapp(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(XappConsts.DbTablePrefix + "YourEntities", ShootingRangeConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<User>(p =>
            {
                p.ToTable(XappConsts.DbTablePrefix + "Users");
                p.ConfigureByConvention();
                p.Property(x => x.UserName).IsRequired().HasMaxLength(UserConsts.MaxUserNameLength);
                p.Property(x => x.Name).IsRequired().HasMaxLength(UserConsts.MaxNameLength);
            });

            builder.Entity<UserFingerprint>(p =>
            {
                p.ToTable(XappConsts.DbTablePrefix + "UserFingerprints");
                p.ConfigureByConvention();
                p.Property(x => x.Fingerprint);
                p.HasOne<User>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
            });
        }
    }
}
