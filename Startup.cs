using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcSportsClub.Data;
using MvcSportsClub.Models;

namespace MvcSportsClub {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            services.AddDbContext<SportsClubContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Choose here between FakeRepository en EFMemberRepository:
            // whenever an IMemberRepository is needed a EFMemberRepository is created.
            services.AddTransient<IMemberRepository, EFMemberRepository>();
            services.AddTransient<IWorkoutRepository, EFWorkoutRepository>();

            // todo lesson 4-02a: registreer services voor Identity:
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SportsClubContext>();

            // todo lesson 4-09a - testen registreren en check op password
            // todo lesson 4-09b - configureren check op password
            //    (Kan ook als parameter in AddIdentity worden ingesteld.)
            services.Configure<IdentityOptions>(
                options => {
                    // voor lesson 4-09b: Configuration check on password:
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredUniqueChars = 5;
                    options.Password.RequiredLength = 8;
                });

            // todo lesson 4-16c: indien gebruik niet ingelogd is, voor geauthoriseerde acties omleiden naar Login.
            // De default redirect naar Login URL in Identity is: /Account/Login
            // Dit kun je op deze manier wijzigen:
            services.ConfigureApplicationCookie(
                options => {
                    // todo lesson 4-16c configureren:
                    options.LoginPath = "/Users/Login";

                    // todo lesson 4-18 configureren: acces denied
                    options.AccessDeniedPath = "/Users/AccessDenied";
                });

            // todo lesson 5-01a: installeer Microsoft.AspNetCore.Authentication.Google
            //  In package manager console: Install-Package Microsoft.AspNetCore.Authentication.Google -Version 5.0.3
            
            // todo lesson 5-01b: maak voor Google sign-in een client ID. Zie: https://developers.google.com/identity/sign-in/web/sign-in

            // todo lesson 5-02a: enable secret storage voor opslag ClientId en ClientSecret. Zie https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#secret-manager
            // todo lesson 5-02b: store ClientId en ClientSecret. Zie https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-5.0#store-the-google-client-id-and-secret

            // todo lesson 5-03a voeg Google service voor authenticatie toe.
            services
                .AddAuthentication()
                .AddGoogle(options => {
                    // todo lesson 5-03b We kunnen gegevens uit de User Secrets halen:
                    IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];

                });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            , UserManager<IdentityUser> userManager
            , RoleManager<IdentityRole> roleManager
            ) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // todo lesson 4-02b. Add middleware for Authentication 
            app.UseAuthentication();

            // todo lesson 4-02c. Add middleware for Authorization
            app.UseAuthorization();

            // todo lesson 4-15. Seed Identity EF store with roles and users
            UserAndRoleDataInitializer.SeedRoles(roleManager);
            UserAndRoleDataInitializer.SeedUsers(userManager);

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
