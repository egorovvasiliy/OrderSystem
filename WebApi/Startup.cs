using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.LoggerService;
using BLL.OrderService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Filters;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        IWebHostEnvironment Env;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Env.IsDevelopment()) // �������� ������ � json-response
                services.AddControllers(options => {
                    options.Filters.Add(new ExceptionFilter());
                });
            services.AddControllers();
            services.AddSingleton<ILogerService, FileLogerService>(provider => {
                var service = new FileLogerService();
                service.RunTaskEcho();
                return service;
            });
            services.AddSingleton<OrderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, OrderService orderService)
        {
            hostApplicationLifetime.ApplicationStarted.Register(()=> {
                Task.Run(() =>
                {
                    orderService.StartHandlersOrders();
                });
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //--------------------------------��� �������
            app.Use(async (context, next) =>
            {
                var x = context.Request.Path;
                await next.Invoke();
            });
            //------------------------------
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware<CheckResponseStatusMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
