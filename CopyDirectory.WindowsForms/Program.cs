using CopyDirectory.Copier; /// Implements ICopy interface and Copy class 
using Microsoft.Extensions.DependencyInjection; // provides DI
using System;
using System.Windows.Forms;

namespace CopyDirectory.WindowsForms
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigureServices();
            Application.Run(new CopyForm());
        }
        /// <summary>
        /// Using Microsoft Extension Dependency Injection to Resolve Dependency
        /// </summary>
        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICopy, Copy>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
