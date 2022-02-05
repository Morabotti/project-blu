using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectBlu.Controllers.Filters;
using ProjectBlu.Extensions;
using ProjectBlu.Jobs;
using ProjectBlu.Models;
using ProjectBlu.Repositories;
using ProjectBlu.Services;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Settings;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectBlu;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMailService, MailService>();
        services.AddSingleton<IOIDCService, OIDCService>();

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<INewsService, NewsService>();
        services.AddTransient<IWikiService, WikiService>();
        services.AddTransient<IGroupService, GroupService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IDealService, DealService>();
        services.AddTransient<IIssueService, IssueService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IProjectService, ProjectService>();

        services.AddAutoMapper(typeof(Startup));

        services.AddHostedService<MailJobRunner>();
        services.AddHostedService<UserTokenJobRunner>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                JwtSettings settings = Configuration.GetSection("Jwt").Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(settings.Key)
                    )
                };
            });

        services.AddDbContext<ProjectBluContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Database"))
        );

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Role", policy => policy.RequireClaim(
                claimType: "role", UserRole.Admin.ToString())
            );
        });

        if (Environment.IsDevelopment())
        {
            services.AddSwagger();
        }

        services.AddControllers(options =>
        {
            options.Filters.Add(new ValidateModelFilter());
            options.Filters.Add(new HttpResponseExceptionFilter());
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/build";
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IOIDCService>();

        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<ProjectBluContext>();
            context.Database.Migrate();
        }

        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.UseRouting();

        app.UseStaticFiles();
        app.UseSpaStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}"
            );
        });

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";
        });
    }
}
