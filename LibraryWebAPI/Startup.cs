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
                                            .AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            //���ofilename=�ɮצW��
                                            .WithExposedHeaders("Content-Disposition");
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
            services.AddScoped<FileService>();


            services.AddControllers().AddNewtonsoftJson();


            //�Ѧ� https://igouist.github.io/post/2021/10/swagger-enable-authorize/
            // ���U Swagger ���;�
            services.AddSwaggerGen(options =>
            {
                // API �A��²��
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Zheng �Ϯ��]��x�t�� API",
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

                // XML�]�w�覡 https://igouist.github.io/post/2021/05/newbie-4-swagger/
                // Ū�� XML �ɮײ��� API ����
                // https://localhost:44323/swagger/index.html ��Authorize���svalue ��J Bearer {JwtToken}
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
