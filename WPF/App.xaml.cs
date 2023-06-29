using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Contracts;
using DAL.Repositories;
using BLL.Services.Contracts;
using BLL.Services;
using WPF.ViewModel;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            var _host = Host.CreateDefaultBuilder()
               .ConfigureServices((hostBuilderContext, serviceCollection) =>
               {
                   serviceCollection.AddDbContext<MyContext>(options =>
                   {
                       options.UseSqlServer("Data Source=ROSTISLAV\\ROSTISLAV;Initial Catalog = Project_Spoti; Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                   }, ServiceLifetime.Singleton);
                   serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();
                   serviceCollection.AddSingleton<IArtistRepository, ArtistRepository>();
                   serviceCollection.AddSingleton<IArtistManager, ArtistManager>();
                   serviceCollection.AddSingleton<ArtistViewModel>();

                   serviceCollection.AddSingleton<MainWindow>();

               })
               .Build();

            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.DataContext = _host.Services.GetRequiredService<ArtistViewModel>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }

            base.OnExit(e);
        }
    }
}
