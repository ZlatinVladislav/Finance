using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.User;
using Finance.Application.Validations;
using Finance.Infrastructure.Data.Interfaces;
using MediatR;

namespace Finance.Application.CQRS.Querries.User
{
    public class UserDetails
    {
        public class Query : IRequest<Result<UserProfileDto>>
        {
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<UserProfileDto>>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;

            public Handler(IAppUserRepository appUserRepository,
                IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }

            public async Task<Result<UserProfileDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUserByUrlUsername(request.UserName);
                if (user == null) return null;
                var userProfileDto = _mapper.Map<UserProfileDto>(user);

                return Result<UserProfileDto>.Success(userProfileDto);
            }
        }
    }
}