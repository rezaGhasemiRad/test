using Mail.Models;
using Mail.Models.Consumers;
using Mail.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Mail
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "RBAC", Version = "v1"});
            });
            // services.AddSingleton<ICusomerWorker, CusomerWorker>();
            services.AddHostedService<ProducerBackGroundService>();

            services.AddMassTransit(conf =>
            {
                conf.AddConsumer<MailConsumer>();
                conf.UsingRabbitMq((busRegisterationContext, config) =>
                {
                    config.Host("amqp://guest:guest@localhost:5672");
                    config.ReceiveEndpoint("test-queue", c =>
                    {
                        c.ConfigureConsumer<MailConsumer>(busRegisterationContext);
                    });
                });
            });
            
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RBAC v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}