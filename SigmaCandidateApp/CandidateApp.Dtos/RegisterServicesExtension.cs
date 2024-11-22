using CandidateApp.Dtos.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateApp.Dtos
{
    public static class RegisterServicesExtension
    {
        public static void AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly); // This will scan the Core assembly for AutoMapper profiles

            
        }
    }
}
