using System.Security.Claims;
using Finance.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Security
{
    public class UserAccessor : IUserAccesor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue((ClaimTypes.Name));
        }
    }
}