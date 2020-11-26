
using MailBox.Database;
using MailBox.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using FluentValidation.AspNetCore;
using FluentValidation;
using MailBox.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using MailBox.Filters;

namespace MailBox
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
            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));

            services.Configure<OpenIdConnectOptions>(
                AzureADB2CDefaults.OpenIdScheme, options =>
                {
                    options.Events = new OpenIdConnectEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var db = context.HttpContext.RequestServices.GetRequiredService<MailBoxDBContext>();
                            string signedInUserID = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                            string email = context.Principal.Identities.First().Claims.Where(x => x.Type == "emails").First().Value;
                            var user = db.Users.Where(x => x.Email == email).FirstOrDefault();
                            if (user == null)
                            {
                                string firstName = context.Principal.Identities.First().Claims.Where(x => x.Type == ClaimTypes.GivenName).First().Value;
                                string lastName = context.Principal.Identities.First().Claims.Where(x => x.Type == ClaimTypes.Surname).First().Value;
                                var role = db.Roles.Where(x => x.ID == 1).FirstOrDefault();
                                User usr = new User { FirstName = firstName, LastName = lastName, Email = email, Role = role };
                                db.Users.Add(usr);
                                db.SaveChanges();

                                user = db.Users.Where(x => x.Email == email).FirstOrDefault();
                            }

                            var nameIdentifier = context.Principal.Identities.First().Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First();
                            context.Principal.Identities.First().RemoveClaim(nameIdentifier);
                            context.Principal.Identities.First().AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));

                            return Task.CompletedTask;
                        },


                    };
                });

            services.AddDbContext<MailBoxDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUserService, UserService>();

            services.AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>());
            

            services.AddControllersWithViews();
            services.AddRazorPages();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
