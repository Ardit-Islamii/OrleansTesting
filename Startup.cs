using Microsoft.OpenApi.Models;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansTesting.Grains;

namespace OrleansTesting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestAppSomething", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestAppSomething v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public static Action<ISiloBuilder> ConfigureOrleans(IConfiguration config)
        {
            return siloBuilder =>
            {
                siloBuilder.Configure((Action<ClusterOptions>)(o =>
                {
                    o.ClusterId = "dev";
                    o.ServiceId = "dev";
                }));
                siloBuilder.UseAdoNetClustering(o =>
                {
                    o.Invariant = "Npgsql";
                    o.ConnectionString = config.GetConnectionString("DatabaseConnectionString");
                });
                siloBuilder.AddAdoNetGrainStorage("testStorage", o =>
                {
                    o.Invariant = "Npgsql";
                    o.ConnectionString = config.GetConnectionString("DatabaseConnectionString");
                    o.UseJsonFormat = true;
                });
                siloBuilder.ConfigureApplicationParts
                (
                    parts => parts.AddApplicationPart(typeof(BetGrain).Assembly).WithReferences()
                );
                siloBuilder.ConfigureEndpoints
                (
                    siloPort: 11111,
                    gatewayPort: 30000,
                    listenOnAnyHostAddress: true
                );
            };
        }
    }
}
