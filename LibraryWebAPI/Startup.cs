using LibraryWebAPI.Wappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Application.Services;
using Zheng.Infrastructure.Data;

namespace LibraryWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //�[�JSession�A��
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "Library.Session"; //�o��|�e�{�bcookie name
                options.IdleTimeout = TimeSpan.FromMinutes(10); //�]�w�h�֮ɶ��L��
                options.Cookie.HttpOnly = true; //�]�w��Cookie�O�_�i�H�z�Lclient�ݸ}���o�e
                options.Cookie.IsEssential = true; //�]�w��Cookie�O�_�����ε{�������n
            });


            //�ҥ� CORS (���l�ӷ��n�D)
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:44319",
                                                          "https://localhost:44323")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                  });
            });



            //��Ʈw�s�u
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //���U�A��
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISessionWapper, SessionWapper>();
            services.AddScoped<AccountService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            //CORS (���l�ӷ��n�D)
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();


            //Session�A��
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
