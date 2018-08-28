using Anet.Web;
using AutoMapper;
using Bookist.Core;
using Bookist.Core.Repositories;
using Bookist.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Bookist.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Mapper.Initialize(cfg => cfg.AddProfile<Mappings>());
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAnetWeb().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<DefaultDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), builder => builder.MigrationsAssembly("Bookist.WebApi"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireRole("User"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("All", policy =>
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                );
            });

            services.AddQiniu(option =>
            {
                Configuration.Bind("Qiniu", option);
            });

            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));

            services.AddTransient<BookService>();
            services.AddTransient<TagService>();
            services.AddTransient<UserService>();

            services.AddTransient<BookRepository>();
            services.AddTransient<LinkRepository>();
            services.AddTransient<TagRepository>();
            services.AddTransient<UserRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("All");

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
