using BL.Interfaces;
using BL.MappingProfile;
using BL.Services;
using DAL.Context;
using DAL.Interfaces;
using DAL.Repopsitories;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DAL.UserModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApi.Services;

namespace WebApi
{
    public class RegisterServicesHelper
    {
        public static void RegisteredServices(WebApplicationBuilder builder) 
        {
            // Configure Authentication (Cookie)
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.LoginPath = "/Admin/Users/Login"; // Path to login page
            //    options.LogoutPath = "/Admin/Users/Logout"; // Path to logout page
            //    options.AccessDeniedPath = "/Admin/Users/AccessDenied"; // Path to access denied page
            //});




            builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 6;             // Minimum length of 6 characters
                options.Password.RequireDigit = false;            // At least one numeric digit
                options.Password.RequireLowercase = false;        // At least one lowercase character
                options.Password.RequireUppercase = false;        // At least one uppercase character
                options.Password.RequireNonAlphanumeric = false;  // At least one special character
                // User settings
                options.User.RequireUniqueEmail = true;          // Email must be unique

            }).AddEntityFrameworkStores<StudentContext>()
            .AddDefaultTokenProviders();



            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Admin/Account/AccessDenied";
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = "/Admin/Account/Login";
                options.LogoutPath = "/Admin/Account/Logout";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            builder.Services.AddAuthorization();


            // Add Repository to system
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #region Services
            builder.Services.AddScoped<IStudent, StudentService>();
            builder.Services.AddScoped<ISubject, SubjectService>();
            builder.Services.AddScoped<IStudentSubjects, StudentSubjectService>();
            builder.Services.AddScoped<IVwStudentSubjects, VwStudentSubjectsService>();
            builder.Services.AddScoped<IUser, UserServices>();
            #endregion


            // Auto mapper configuration
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            // configure Serilog
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.Console()
            //    .WriteTo.MSSqlServer(
            //        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
            //        tableName: "Log",
            //        autoCreateSqlTable: true)
            //    .CreateLogger();


            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Log",
                            AutoCreateSqlTable = true
                        })
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
