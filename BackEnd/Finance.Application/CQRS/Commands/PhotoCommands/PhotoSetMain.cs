using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Validations;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Interfaces.Base;
using MediatR;

namespace Finance.Application.CQRS.Commands.PhotoCommands
{
    public class PhotoSetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IBaseRepository<Transaction> _transactionRepository;

            public Handler(IBaseRepository<Transaction> genericRepository,
                IAppUserRepository appUserRepository)
            {
                _transactionRepository = genericRepository;
                _appUserRepository = appUserRepository;
            }


            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUserPhoto();
                if (user == null) return null;
                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);
                if (photo == null) return null;
                var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
                if (currentMain != null) currentMain.IsMain = false;
                photo.IsMain = true;
                var success = await _transactionRepository.SaveChanges();
                return success
                    ? Result<Unit>.Success(Unit.Value)
                    : Result<Unit>.Failure("Error while setting photo main");
            }
        }
    }
}