using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DtoModels.User;
using Finance.Application.Validations;
using Finance.Application.Validations.UserValidators;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Interfaces.Base;
using FluentValidation;
using MediatR;

namespace Finance.Application.CQRS.Commands.UserCommands
{
    public class UserDescriptionUpdate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public UserDescriptionDto UserDescriptionDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserDescriptionDto).SetValidator(new UserDescriptionValidation());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IMapper _mapper;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IBaseRepository<UserDescription> _baseRepository;

            public Handler(IBaseRepository<UserDescription> baseRepository,
                IMapper mapper,  IAppUserRepository appUserRepository)
            {
                _baseRepository = baseRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var descriptionModel = _mapper.Map<UserDescription>(request.UserDescriptionDto);
                var user = await _appUserRepository.GetUser();
                descriptionModel.UserDescriptionId = user.Id;
                await _baseRepository.Put(descriptionModel);
                var result = await _baseRepository.SaveChanges();

                return !result
                    ? Result<Unit>.Failure("Failed to update description.")
                    : Result<Unit>.Success(Unit.Value);
            }
        }
    }
}