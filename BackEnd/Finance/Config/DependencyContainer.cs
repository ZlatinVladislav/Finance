using Finance.Application.Interfaces;
using Finance.Application.Security;
using Finance.Application.Services;
using Finance.Domain.Interfaces;
using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Repositories;
using Finance.Infrastructure.Data.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Config
{
    public static class DependencyContainer
    {
        public static void AddIoCService(this IServiceCollection services)
        {
            // IoC - Inversion Of Control
            // Application
            services.AddScoped<IUserAccesor, UserAccessor>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
          //  services.AddScoped<ITransactionTypeService, TransactionTypeService>();


            // Domain.Interfaces > Infrastructure.Data.Repositories
            services.AddScoped<IBaseRepository<Transaction>, BaseRepository<Transaction>>();
            services.AddScoped<IBaseRepository<TransactionType>, BaseRepository<TransactionType>>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
        }
    }
}
