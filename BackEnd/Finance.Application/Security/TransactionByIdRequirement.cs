using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finance.Domain.Interfaces;
using Finance.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Security
{
    public class TransactionByIdRequirement : IAuthorizationRequirement
    {
    }

    public class IsUserRequirementHandler : AuthorizationHandler<TransactionByIdRequirement>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly FinanceDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionRepository _transactionRepository;

        public IsUserRequirementHandler(FinanceDBContext dbContext, IHttpContextAccessor httpContextAccessor,
            ITransactionRepository transactionRepository, IAppUserRepository appUserRepository)
        {
            _transactionRepository = transactionRepository;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _appUserRepository = appUserRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            TransactionByIdRequirement byIdRequirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null) return Task.CompletedTask;

            var tansactionId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value.ToString());

            var transaction = _transactionRepository.GetTransactionsForeignDataByIdNoTracking(tansactionId).Result;

            if (transaction == null) return Task.CompletedTask;

            if (userId.Value == transaction.AppUser.Id) context.Succeed(byIdRequirement);
            return Task.CompletedTask;
        }
    }
}