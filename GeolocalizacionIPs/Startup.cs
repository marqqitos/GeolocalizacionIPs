using System;
using AutoMapper;
using GeolocalizacionIPs.DBContext;
using GeolocalizacionIPs.Mapping;
using GeolocalizacionIPs.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GeolocalizacionIPs.Constants;
using GeolocalizacionIPs.Repositories;
using GeolocalizacionIPs.Repositories.Interfaces;
using GeolocalizacionIPs.ServicesExternal;
using GeolocalizacionIPs.ServicesExternal.Interfaces;
using System.Net.Http;
using GeolocalizacionIPs.Services;
using GeolocalizacionIPs.Services.Interfaces;

namespace GeolocalizacionIPs
{
    public class Startup
    {
        public IPToCountrySettings IPToCountrySettings { get; }
        public RestCountriesSettings RestCountriesSettings { get; }
        public FixerSettings FixerSettings { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var externalServicesConfig = configuration.GetSection("ExternalServices");
            IPToCountrySettings = externalServicesConfig.GetSection("IPToCountry").Get<IPToCountrySettings>();
            RestCountriesSettings = externalServicesConfig.GetSection("RestCountries").Get<RestCountriesSettings>();
            FixerSettings = externalServicesConfig.GetSection("Fixer").Get<FixerSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            services.AddEntityFrameworkNpgsql().AddDbContext<GeolocalizacionIPsContext>(opt => opt.UseNpgsql(connectionString, providerOptions => providerOptions.EnableRetryOnFailure(5)));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new IPInfoMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient(ExternalServicesClientName.IP_TO_COUNTRY_HTTP_CLIENT_NAME, client =>
            {
                client.BaseAddress = new Uri(IPToCountrySettings.BaseAddress);
            });

            services.AddHttpClient(ExternalServicesClientName.REST_COUNTRIES_HTTP_CLIENT_NAME, client =>
            {
                client.BaseAddress = new Uri(RestCountriesSettings.BaseAddress);
            });

            services.AddHttpClient(ExternalServicesClientName.FIXER_HTTP_CLIENT_NAME, client =>
            {
                client.BaseAddress = new Uri(FixerSettings.BaseAddress);
            });

            services.AddScoped<IDistanciaService, DistanciaService>();

            services.AddScoped<IIPToCountryService, IPToCountryService>();
            services.AddScoped<IRestCountriesService, RestCountriesService>();
            services.AddScoped<IFixerService, FixerService>(s => new FixerService(s.GetService<IHttpClientFactory>(), FixerSettings));

            services.AddScoped<IIPInfoRepository, IPInfoRepository>();
            services.AddScoped<IIPInfoService, IPInfoService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
