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

            //啟用 CORS (跨原始來源要求)
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

            //JWT 設定
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],// 簽發者
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],// 接收者
                    ValidateLifetime = true,// 驗證時間
                    ClockSkew = TimeSpan.Zero,//時間偏移，確定發行到期
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:KEY"]))// Key
                };
            });

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));
            //註冊HttpClient
            services.AddHttpClient();
            //資料庫連線
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<JwtHelper>();
            //SMTP寄信服務
            services.AddSingleton<EmailSenderHelper>();

            services.AddScoped<AccountService>(); //帳號
            services.AddScoped<BookService>(); //書籍
            services.AddScoped<BookPhotoService>(); //書籍圖片
            services.AddScoped<ILoanService, LoanService>(); //借閱書籍

            //登入的帳號資訊
            services.AddTransient<IUserService, UserService>();
            //檔案上傳
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

            //CORS (跨原始來源要求)
            app.UseCors(MyAllowSpecificOrigins);

            app.UseCookiePolicy();

            //啟用驗證
            app.UseAuthentication();
            //授權身分
            app.UseAuthorization();

            //靜態檔案，用來讀取路徑
            app.UseStaticFiles(new StaticFileOptions
            {
                //在wwwroot資料夾底下，另外設定路徑
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
