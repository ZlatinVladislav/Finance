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
    public class IsUserRequirement : IAuthorizationRequirement
    {
    }

    public class IsUserRequirementHandler : AuthorizationHandler<IsUserRequirement>
    {
        private readonly FinanceDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppUserRepository _appUserRepository;
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
            IsUserRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null) return Task.CompletedTask;

            var tansactionId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value.ToString());

            var transaction = _transactionRepository.GetTransactionsForeignDataById(tansactionId).Result;

            if (transaction == null) return Task.CompletedTask;

            if (userId.Value == transaction.AppUser.Id) context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}