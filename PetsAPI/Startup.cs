using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetsAPI.Database;
using PetsAPI.Models;


namespace PetsAPI
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
            .AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<MascotasDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

            services.AddCors(o => o.AddPolicy("customsPermitions", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

			//var dbstring = ConfigurationExtensions.GetConnectionString(this.Configuration, "azuredb");
			var dbstring = ConfigurationExtensions.GetConnectionString(this.Configuration, "azuredb");
			services.AddDbContext<MascotasDbContext>(options => options.UseSqlServer(dbstring));
            services.AddMvc();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseCors("customsPermitions");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new Role { Name = roleName });
                }
            }

            var superAdmin = new User {UserName = Configuration["AppSettings:UserName"], Email = Configuration["AppSettings:UserEmail"]};
            string userPWD = Configuration["AppSettings:UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:UserEmail"]);
            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(superAdmin, userPWD);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(superAdmin, "Admin");
                }
            }
        }
    }
}
