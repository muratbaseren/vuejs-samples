using AutoMapper;
using BlogVue.WebAPI.Business.Managers;
using BlogVue.WebAPI.DataAccess.Mongo.Context;
using BlogVue.WebAPI.DataAccess.Mongo.Repositories;
using BlogVue.WebAPI.Filters;
using MFramework.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace BlogVue.WebAPI
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
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.Configure<EMailSender>(appSettingsSection);
            services.AddSingleton<IEMailSender, EMailSender>();

            string jwtKey = Configuration["Authentication:JWTKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtKey)),
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddScoped<ValidateModelAttribute>();
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest);

            // services.AddCors(options =>
            // {
            //     options.AddPolicy("_cors_settings",
            //         builder => builder.WithOrigins("http://localhost:5000", "https://localhost:5001").WithMethods("GET"));

            //     options.AddPolicy("AllowOrigin",
            //         builder => builder.AllowAnyOrigin());
            // });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Blog Vue API",
                    Version = "1.0",
                    Contact = new OpenApiContact() { Name = "Blog Vue", Email = "contact@blogvue.com" },
                    //License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });

                //options.IncludeXmlComments(XmlCommentsFilePath);
                options.UseInlineDefinitionsForEnums();
                options.SchemaFilter<SwaggerEnumSchemaFilter>();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //    {
                //        {
                //          new OpenApiSecurityScheme
                //          {
                //            Reference = new OpenApiReference
                //              {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //              },
                //              Scheme = "oauth2",
                //              Name = "Bearer",
                //              In = ParameterLocation.Header,

                //            },
                //            new List<string>()
                //          }
                //    });
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAlbumManager, AlbumManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ITagManager, TagManager>();
            services.AddScoped<IAuthorManager, AuthorManager>();
            services.AddScoped<IBlogPostManager, BlogPostManager>();

            services.AddScoped<IMongoAlbumRepository, MongoAlbumRepository>();
            services.AddScoped<IMongoCategoryRepository, MongoCategoryRepository>();
            services.AddScoped<IMongoTagRepository, MongoTagRepository>();
            services.AddScoped<IMongoAuthorRepository, MongoAuthorRepository>();
            services.AddScoped<IMongoBlogPostRepository, MongoBlogPostRepository>();

            services.AddScoped<MongoDBContext>();
            services.AddScoped<IMongoDBInitializer, MongoDBInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseCors("_cors_settings");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
            });
        }

        //static string XmlCommentsFilePath
        //{
        //    get
        //    {
        //        var basePath = PlatformServices.Default.Application.ApplicationBasePath;
        //        var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
        //        return Path.Combine(basePath, fileName);
        //    }
        //}
    }
}
