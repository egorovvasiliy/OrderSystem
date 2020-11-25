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
        ILogerService logerService;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Env.IsDevelopment()) // Просмотр ошибок в json-response
                services.AddControllers(options => {
                    options.Filters.Add(new ExceptionFilter(logerService));
                });
            services.AddControllers();
            services.AddSingleton<ILogerService, FileLogerService>();
            services.AddSingleton<OrderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, OrderService orderService, ILogerService _logerService)
        {
            logerService = _logerService;
            hostApplicationLifetime.ApplicationStarted.Register(()=> {
                orderService.StartHandlersOrders();
                _logerService.RunTaskEcho();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //--------------------------------Для отладки
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
