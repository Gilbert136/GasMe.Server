using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GasMe.Data;
using GasMe.Api.Hubs;
using GasMe.Service.Options;
using GasMe.Service;

namespace GasMe.Api
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
            var jwtSettings = new JwtSettings();
            Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer( Configuration.GetConnectionString("SqlAzureConnection")); });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews().AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });
            services.AddHttpContextAccessor();

            var tokenValidationParameters = new TokenValidationParameters{
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddSignalR();
            services.AddSwaggerGen(x => {
                var security = new Dictionary<string, IEnumerable<string>>{
                    {"Bearer", new string[0]}
                };
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                    Description = "JWT Authorisation header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(
                    new OpenApiSecurityRequirement{
                        {
                        new OpenApiSecurityScheme{
                                Reference = new OpenApiReference 
                                { 
                                    Type = ReferenceType.SecurityScheme, 
                                    Id = "Bearer" 
                                }
                            }, new string[] {}
                        }
                    });
                });
            
            services.AddScoped<ICapacityService, CapacityService>(); 
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description); });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<OrderHub>("hubs/order");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
