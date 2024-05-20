using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using InsiteTeamTask.Data.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using InsiteTeamTask.Services;

namespace InsiteTeamTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IDataProvider, DataProvider>();

			var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();

			services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

			services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors();
			app.UseMiddleware<ApiKeyMiddleware>();
			app.UseHttpsRedirection();
			app.UseMvc();
        }
    }
}