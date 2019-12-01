using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 9;
                option.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;

                var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();

                option.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("DeleteRolePolicy",
                //    policy => policy.RequireClaim("Delete Role", "true"));

                options.AddPolicy("DeleteRolePolicy",
                  policy => policy.RequireAssertion(context =>
                      (context.User.IsInRole("Admin") &&
                      context.User.HasClaim(claim => claim.Type == "Delete Role" && claim.Value == "true")) ||
                      context.User.IsInRole("Super Admin")
                  ));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireAssertion(context =>
                        (context.User.IsInRole("Admin") &&
                        context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true")) ||
                        context.User.IsInRole("Super Admin")
                    ));

                //options.AddPolicy("AdminRolePolicy",
                //    policy => policy.RequireRole("Admin"));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireAssertion(context =>
                        context.User.IsInRole("Admin") || context.User.IsInRole("Super Admin")
                    ));
            });

            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
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
                app.UseExceptionHandler("/Error");

                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}