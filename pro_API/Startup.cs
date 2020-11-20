using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pro_API.Data;
using AutoMapper;
using pro_API.MapperProfiles;
using pro_API.Repositories;

namespace pro_API
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
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options => options
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Options => Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                }
                );
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IVocRepository, VocRepository>();
            services.AddScoped<IPickNewRepository, PickNewRepository>();
            services.AddScoped<IUserVocRepository, UserVocRepository>();
            services.AddScoped<IVocMasterRepository, VocMasterRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ILangRepository, LangRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISubtitleRepository, SubtitleRepository>();
            services.AddScoped<IInfluencerRepository, InfluencerRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();
            services.AddScoped<IIdiomRepository, IdiomRepository>();
            services.AddScoped<IPhraseRepository, PhraseRepository>();
            services.AddScoped<ISearchResultRepository, SearchResultRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
