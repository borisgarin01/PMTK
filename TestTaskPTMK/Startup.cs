using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestTaskPTMK.Repositories.Classes;
using TestTaskPTMK.Repositories.Interfaces.Base;

namespace TestTaskPTMK
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddTransient<IPeopleRepository, PeopleRepository>(peopleRepository => new PeopleRepository(connectionString));
            services.AddTransient<ISexesRepository, SexesRepository>(sexesRepository => new SexesRepository(connectionString));
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMvcWithDefaultRoute();
        }
    }
}