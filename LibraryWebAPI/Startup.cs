using LibraryWebAPI.Helpers;
using LibraryWebAPI.Interfaces;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;
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

            //�ҥ� CORS (���l�ӷ��n�D)
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://127.0.0.1:8080",
                                                          "http://localhost:8080",
                                                          "https://localhost:44323",
                                                          "https://localhost:44331")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                  });
            });

            //JWT �]�w
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],// ñ�o��
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],// ������
                    ValidateLifetime = true,// ���Үɶ�
                    ClockSkew = TimeSpan.Zero,//�ɶ������A�T�w�o����
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:KEY"]))// Key
                };
            });

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));
            //���UHttpClient
            services.AddHttpClient();
            //��Ʈw�s�u
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<JwtHelper>();
            //SMTP�H�H�A��
            services.AddSingleton<EmailSenderHelper>();

            services.AddScoped<AccountService>(); //�b��
            services.AddScoped<BookService>(); //���y
            services.AddScoped<BookPhotoService>(); //���y�Ϥ�
            services.AddScoped<ILoanService, LoanService>(); //�ɾ\���y

            //�n�J���b����T
            services.AddTransient<IUserService, UserService>();
            //�ɮפW��
            services.AddScoped<UploadFileService>();
            services.AddScoped<OpenDataService>();

            services.AddControllers().AddNewtonsoftJson();
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

            app.UseCookiePolicy();

            //�ҥ�����
            app.UseAuthentication();
            //���v����
            app.UseAuthorization();

            //�R�A�ɮסA�Ψ�Ū�����|
            app.UseStaticFiles(new StaticFileOptions
            {
                //�bwwwroot��Ƨ����U�A�t�~�]�w���|
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Uploads")),
                RequestPath = "/StaticFiles"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
