using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorld
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddSingleton<IHello, Hello>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            app.Map("/test", testPipeline);
            app.Use(next => async context =>
              {
                  await context.Response.WriteAsync("Before Hello: ");
                  await next.Invoke(context);
              });
            //Hello hello = new Hello();
            app.Run(async (context) =>
            {
              //  throw new Exception("Hello Exception");
                await context.Response.WriteAsync("Hello World");
            });
        }

        private static void testPipeline(IApplicationBuilder app)
        {
            app.MapWhen(context => { return context.Request.Query.ContainsKey("ln"); }, testPipeline1);
            app.MapWhen(context => { return context.Request.Query.ContainsKey("q"); }, testPipeline2);
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hell from test Mapping!");
            });
        }
        private static void testPipeline1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hell from testPipeline1");
            });
        }
        private static void testPipeline2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hell from testPipeline2");
            });
        }
    }
}
