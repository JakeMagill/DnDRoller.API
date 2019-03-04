using System;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using DnDRoller.API.Application.Mappers;
using DnDRoller.API.Application.Services;
using Microsoft.Extensions.Configuration;
using DnDRoller.API.Application.Interfaces;
using DnDRoller.API.Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DnDRoller.API
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
            var connection = "Server=tcp:pazzda.database.windows.net,1433;Initial Catalog=DnDRoller;Persist Security Info=False;User ID=jake.magill;Password=Chelsea18!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var mappingConfig = new MapperConfiguration(x => 
            {
                x.AddProfile(new UserMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            //Adding CORS
            services.AddCors();

            //Adding Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            //Adding Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "DnDRoller API", Version = "v1" });
            });

            //Adding DbContext
            services.AddDbContext<DatabaseService>(options =>
                options.UseSqlServer(connection, x => x.MigrationsAssembly("DnDRoller.API.Infrastructure")));

            services.AddSingleton(mapper);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Use swagger if in a dev environment
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("./v1/swagger.json", "DnDRoller API V1");
                });
            }

            app.UseCors(x => x
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
