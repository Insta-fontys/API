using API.Services;
using API.Services.Interfaces;
using DataAccesLibrary.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string IdentityDatabaseConnectionString { get; set; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("Default"));
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });


            IdentityDatabaseConnectionString = Configuration.GetConnectionString("ConnectionString");

            services.AddDbContext<IdentityDatabaseContext>(options => options.UseMySql(IdentityDatabaseConnectionString,
                ServerVersion.AutoDetect(IdentityDatabaseConnectionString)));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Paswword must be atleast 8 characters long
                // must contain a digit, uppercase, lowercase and 1 special character
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<IdentityDatabaseContext>()
               .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("DSEFSDF324dsfsd!@QWDF3erf#")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IRegisterDatabase, RegisterDatabaseService>();
            services.AddTransient<ILoginDatabase, LoginDatabaseService>();
            services.AddTransient<IPostDatabase, PostDatabaseService>();
            services.AddTransient<IReactionDatabase, ReactionDatabaseService>();
            services.AddTransient<ILikePostsDatabase, LikePostDatabaseService>();
            services.AddTransient<ITokenDatabase, TokenDatabaseService>();
            services.AddTransient<IFollowDatabase, FollowDatabaseService>();

            services.AddTransient<IdentityRegistrationService>();
            services.AddTransient<AuthenticationService>();
            services.AddTransient<UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseCors(MyAllowSpecificOrigins);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

           


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Init(serviceProvider).Wait();

        }

        private static async Task Init(IServiceProvider serviceProvider)
        {
            //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ////create correct roles
            //if (!await roleManager.RoleExistsAsync("admin"))
            //    await roleManager.CreateAsync(new IdentityRole("admin"));
            //if (!await roleManager.RoleExistsAsync("fan"))
            //    await roleManager.CreateAsync(new IdentityRole("fan"));
            //if (!await roleManager.RoleExistsAsync("creator"))
            //    await roleManager.CreateAsync(new IdentityRole("creator"));
        }
    }
}
