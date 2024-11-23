using CandidateApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using CandidateApp.Business;
using CandidateApp.Dtos;
using CandidateApp.DataAccess.DataRepositories;


namespace CandidateApp.API.Infrastructures
{
    public static class ConfigBuilderService
    {
        public static IServiceCollection UseCustomBuilderServices(this IServiceCollection services, IConfiguration configuration)
        {


            // Add Swagger services
            services.AddEndpointsApiExplorer(); // Adds support for API explorer
                                                        //var xmlFile = Path.Combine(AppContext.BaseDirectory, "YourProjectName.xml");
            services.AddSwaggerGen(options =>
            {
                //options.IncludeXmlComments(xmlFile); // Include the XML comments in Swagger
            }); // Adds Swagger generation services



            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IDataRepository<>), typeof(SqlRepository<>));


            services.AddAutoMapperServices();

            // services.AddScoped<IJobCandidateAppService, JobCandidateAppServices>();

            services.RegisterScopedServices();

            return services;
        }
    }
}
