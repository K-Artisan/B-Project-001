using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Zo.Xapp.EntityFrameworkCore
{

    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands)
     * 
     
     参考资料：
     https://docs.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
     从设计时工厂创建DbContext：
     你还可以通过实现接口来告诉工具如何创建 DbContext IDesignTimeDbContextFactory<TContext> ：
     如果实现此接口的类在与派生的项目相同的项目中 DbContext 
     或在应用程序的启动项目中找到，
     则这些工具将绕过创建 DbContext 的其他方法，并改用设计时工厂。
     
     如果需要以不同于运行时的方式配置 DbContext 的设计时，则设计时工厂特别有用 DbContext 。如果构造函数采用其他参数，
     但未在 di 中注册，如果根本不使用 di，
     或者出于某种原因而不是使用 CreateHostBuilder ASP.NET Core 应用程序的类中的方法 Main 。
     
     
     总之一句话：
     实现了IDesignTimeDbContextFactory<BookStoreMigrationsDbContext>，
     就可以使用命令行执行数据库迁移,
        (1).在 NET Core CLI中执行： dotnet ef database update
        (2).在 Visual Studio中执行：Update-Database 
     
     */

    public class XappMigrationsDbContextFactory : IDesignTimeDbContextFactory<XappMigrationsDbContext>
    {
        public XappMigrationsDbContext CreateDbContext(string[] args)
        {
            XappEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();
            var connectionString = configuration.GetConnectionString(XappConsts.ConnectionStringName);

            var builder = new DbContextOptionsBuilder<XappMigrationsDbContext>()
            //.UseSqlServer(connectionString);

            /*-------------------------------使用MySQL数据库------------------------------
            //https://github.com/abpframework/abp/blob/baec4745e424fea128c4575df3743ad4b50c2bec/docs/en/Entity-Framework-Core-MySQL.md
            //https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/pull/1233
            //MySQL数据库连接字符串： https://www.connectionstrings.com/mysql/ ，
                看MySQL Connector/Net: https://www.connectionstrings.com/mysql-connector-net-mysqlconnection/
            //mysql数据库版本查看方法：
            //安装mysql后，有一个名为：mysql的数据库，选中，查询：select version()， 比如输出：5.6.34-log，即是数据库版本
            */
            //.UseMySql(connectionString, ServerVersion.FromString("5.6.34-log"));
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); //自动检测，推荐

           /*-------------------------------使用MySQL数据库------------------------------*/

            return new XappMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()                           
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Zo.Xapp.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
