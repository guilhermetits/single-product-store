using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SingleProductStore.Business.Contract.Service;
using SingleProductStore.Business.Service;
using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Data.Sql.Repository;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Web.Models.Mapper;

namespace SingleProductStore
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
            services.AddMvc();
            services.AddAutoMapper((config) => config.AddProfile<SpsMapperProfile>());
            services.AddDbContext<SpsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SpsDatabase")));
            services.AddScoped<IDbContext>(provider => provider.GetService<SpsContext>());
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IPromotionService, PromotionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
