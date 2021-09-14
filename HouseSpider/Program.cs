using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HouseSpider.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace HouseSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Info("System Init");
            //启动爬虫后台
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
            Console.WriteLine("Web API Server has started at http://localhost:5000");
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5004")
                .UseConfiguration(config)
                .UseStartup<Startup>();
        }
    }

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
            services.AddMvcCore().AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
    public class MyEndpoint : Controller
    {
        [Route("houseinfo")]
        public IActionResult Get(string HouseLink)
        {
            try
            {
                Logger.Info(HouseLink);
                var houseInfo = HouseInfoCollection.GetHouseInfo(HouseLink);
                Logger.Info(HouseLink);
                //Common.InsertUserInfoRecord(userInfo);
                return new JsonResult(houseInfo);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return new BadRequestResult();
            }
        }
    }
}


