using Finance.Application.Interfaces;
using Finance.Application.Services.Photos;
using Finance.Application.Services.Security.Base;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Interfaces.Base;
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

            // Domain.Interfaces > Infrastructure.Data.Repositories
            services.AddScoped<IBaseRepository<Transaction>, BaseRepository<Transaction>>();
            services.AddScoped<IBaseRepository<TransactionType>, BaseRepository<TransactionType>>();
            services.AddScoped<IBaseRepository<Bank>, BaseRepository<Bank>>();
            services.AddScoped<IBaseRepository<BankTransaction>, BaseRepository<BankTransaction>>();
            services.AddScoped<IBaseRepository<UserDescription>, BaseRepository<UserDescription>>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
        }
    }
}