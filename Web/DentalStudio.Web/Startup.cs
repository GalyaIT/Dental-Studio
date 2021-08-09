namespace DentalStudio.Web
{
    using System.Globalization;
    using System.Reflection;
    using CloudinaryDotNet;
    using DentalStudio.Data;
    using DentalStudio.Data.Common;
    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Data.Repositories;
    using DentalStudio.Data.Seeding;
    using DentalStudio.Services;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Messaging;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels;
    using DentalStudio.Web.ViewModels.Administration.Appointments;
    using DentalStudio.Web.ViewModels.Administration.Doctors;
    using DentalStudio.Web.ViewModels.Administration.Patients;
    using DentalStudio.Web.ViewModels.Administration.Procedures;
    using DentalStudio.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => false;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // CSRF
            });
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services

            Account cloudinaryCredentials = new Account(
               this.configuration["Cloudinary:CloudName"],
               this.configuration["Cloudinary:ApiKey"],
               this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:SendGridApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IDoctorsService, DoctorsService>();
            services.AddTransient<IPatientsService, PatientsService>();
            services.AddTransient<IProceduresService, ProceduresService>();
            services.AddTransient<IAppointmentsService, AppointmentsService>();
            services.AddTransient<IPostsService, PostsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(DoctorCreateInputModel).GetTypeInfo().Assembly,
                typeof(DoctorEditViewModel).GetTypeInfo().Assembly,
                typeof(DoctorServiceModel).GetTypeInfo().Assembly,
                typeof(PatientCreateInputModel).GetTypeInfo().Assembly,
                typeof(PatientServiceModel).GetTypeInfo().Assembly,
                typeof(ProcedureCreateInputModel).GetTypeInfo().Assembly,
                typeof(ProcedureServiceModel).GetTypeInfo().Assembly,
                typeof(AppointmentServiceModel).GetTypeInfo().Assembly,
                typeof(AppointmentViewModel).GetTypeInfo().Assembly,
                typeof(PostCreateInputModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
