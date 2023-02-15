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
using Zheng.Infra.Data.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
                                            .AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            //取得filename=檔案名稱
                                            .WithExposedHeaders("Content-Disposition");
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
            services.AddScoped<FileService>();


            services.AddControllers().AddNewtonsoftJson();


            //參考 https://igouist.github.io/post/2021/10/swagger-enable-authorize/
            // 註冊 Swagger 產生器
            services.AddSwaggerGen(options =>
            {
                // API 服務簡介
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Zheng 圖書館後台系統 API",
                    Description = "hezhengmin API",
                    TermsOfService = new Uri("https://hezhengmin.github.io/"),
                    Contact = new OpenApiContact
                    {
                        Name = "zhengmin",
                        Email = string.Empty,
                        Url = new Uri("https://hezhengmin.github.io/"),
                    }
                });

                // Authorization
                options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization"
                });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    });

                // XML設定方式 https://igouist.github.io/post/2021/05/newbie-4-swagger/
                // 讀取 XML 檔案產生 API 說明
                // https://localhost:44323/swagger/index.html 的Authorize按鈕value 輸入 Bearer {JwtToken}
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Library V1");
            });

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
