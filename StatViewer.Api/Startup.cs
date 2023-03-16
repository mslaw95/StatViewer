using Microsoft.EntityFrameworkCore;
using StatViewer.Api.Services;
using StatViewer.Api.Services.Interfaces;
using StatViewer.Data.DataTransfer;

namespace StatViewer.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextFactory<StoreContext>(o => o.UseInMemoryDatabase("storeDb"));

            services.AddScoped<IStoreService, StoreService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(o => o.AllowAnyOrigin());
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
