using LicentaApi.Data;
using LicentaApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using LicentaApi.Hashing;
using Microsoft.Net.Http.Headers;
using LicentaApi.Repositories.RestaurantRepository;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using System.IO;
using LicentaApi.Repositories.MenuRepository;
using LicentaApi.Repositories.OrderRepository;

namespace LicentaApi
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
            services.AddCors(opt => opt.AddPolicy("CorsPolicy", c =>
            {
                c.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
            }));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRestaurantRepository,RestaurantRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IJwtToken, JwtToken>();
            services.AddSingleton<IDbContext, DbContext>();
            services.Configure<DbConfig>(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LicentaApi", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["jwt_secret"])),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           }
           );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LicentaApi v1"));
            }
        

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            //      app.UseCors(options => options
            //    .WithOrigins("http://localhost:3000")
            //    .AllowAnyHeader()
            //    //.WithMethods("POST", "GET", "OPTIONS")
            //    .AllowAnyMethod()
            //    //  .SetIsOriginAllowedToAllowWildcardSubdomains()
            //    .AllowCredentials()

            //// .WithHeaders(
            ////HeaderNames.Accept,
            ////HeaderNames.ContentType,
            ////HeaderNames.Authorization)
            ////.WithExposedHeaders("Authorization")

            //);
            app.UseCors("CorsPolicy");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
