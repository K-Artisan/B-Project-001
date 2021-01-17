using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp;
using Zo.Xapp.Data;

namespace Zo.Xapp.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// 
    /// 使用abp 框架
    /// https://docs.abp.io/zh-Hans/abp/latest/Startup-Templates/WPF
    /// 
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly IAbpApplicationWithExternalServiceProvider _application;

        public App()
        {
            _host = Host
                .CreateDefaultBuilder(null)
                .UseAutofac()
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication<WpfModule>();
                }).Build();
            _application = _host.Services.GetService<IAbpApplicationWithExternalServiceProvider>();

            //TODO:暂时还是通过执行迁移程序（Zo.Xapp.DbMigrator）进行数据库迁移
            //MigrateDatabase(_host.Services);
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .CreateLogger();

            try
            {
                Log.Information("Starting WPF host.");
                await _host.StartAsync();
                Initialize(_host.Services);

                _host.Services.GetService<MainWindow>()?.Show();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            _application.Shutdown();
            await _host.StopAsync();
            _host.Dispose();
        }

        private void Initialize(IServiceProvider serviceProvider)
        {
            _application.Initialize(serviceProvider);
        }

        /// <summary>
        /// 执行数据库迁移
        /// </summary>
        /// <param name="serviceProvider"></param>
        private void MigrateDatabase(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<XappDbMigrationService>()
                    .MigrateAsync()
                    .Wait();
        }
    }
}
