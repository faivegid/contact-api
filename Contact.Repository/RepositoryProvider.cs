using AutoMapper.Configuration;
using Contact.Repository.Implementaions;
using Contact.Repository.Interface;
using Contact.Repository.RepoUnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Contact.Repository
{
    public  class RepositoryProvider
    {
        public static void CofigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
