using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WebApiMovies.Data;
using WebApiMovies.Data.Entities;
using WebApiMovies.Data_Access;
using WebApiMovies.Data_Access.Implements;
using WebApiMovies.Middlewares;
using WebApiMovies.Services;
using WebApiMovies.Services.Implements;

namespace WebApiMovies
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            //Add services container

            //Global configuration
            services.AddSingleton(Configuration);

            //CORS
            services.AddCors(opt =>
            {
                opt.AddPolicy("EnableCORS", buidler =>
                {
                    buidler.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //DataBase
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(Configuration["ConnectionString"]!));

            //Identity
            services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireUppercase = true;

                    opt.User.RequireUniqueEmail = true;
                    opt.SignIn.RequireConfirmedEmail = true;

                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    opt.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Authentication
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateActor = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["KeyJWT"]!))
                    };
                });

            //Token Provider Configuration
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));

            //Repositories
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieReviewRepository, MovieReviewRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Automapper
            services.AddAutoMapper(typeof(StartUp));

            //Entities Services 
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMovieReviewService, MovieReviewService>();

            //Upload Files Service
            services.AddTransient<IUploadFileService, UploadFileService>();

            //Cloudinary Instance
            services.AddTransient<Cloudinary>(provider =>
            {
                var cloudinary_url = Configuration["CLOUDINARY_URL"];
                var cloudinary = new Cloudinary(cloudinary_url);
                cloudinary.Api.Secure = true;
                return cloudinary;
            });

            //Email Service
            services.AddScoped<IEmailService, EmailService>();

            //Middleware Exception
            services.AddTransient<ExceptionHandlingMiddleware>();

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseMiddleware<ExceptionHandlingMiddleware>();
    
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("EnableCORS");

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
