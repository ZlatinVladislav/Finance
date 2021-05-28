using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finance.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Services.Security
{
    public class TransactionByIdRequirement : IAuthorizationRequirement
    {
    }

    public class IsUserRequirementHandler : AuthorizationHandler<TransactionByIdRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionRepository _transactionRepository;

        public IsUserRequirementHandler(IHttpContextAccessor httpContextAccessor,
            ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            TransactionByIdRequirement byIdRequirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null) return Task.CompletedTask;

            var transactionId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value.ToString());

            var transaction = _transactionRepository.GetTransactionsForeignDataByIdNoTracking(transactionId).Result;

            if (transaction == null) return Task.CompletedTask;

            if (userId.Value == transaction.AppUser.Id) context.Succeed(byIdRequirement);
            return Task.CompletedTask;
        }
    }
}