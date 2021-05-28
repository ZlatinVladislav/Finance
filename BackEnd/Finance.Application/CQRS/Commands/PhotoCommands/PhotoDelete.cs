using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Interfaces;
using Finance.Application.Validations;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Interfaces.Base;
using MediatR;

namespace Finance.Application.CQRS.Commands.PhotoCommands
{
    public class PhotoDelete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IBaseRepository<Transaction> _transactionRepository;

            public Handler(IBaseRepository<Transaction> genericRepository,
                IAppUserRepository appUserRepository, IPhotoAccessor photoAccessor)
            {
                _transactionRepository = genericRepository;

                _appUserRepository = appUserRepository;
                _photoAccessor = photoAccessor;
            }


            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUserPhoto();
                if (user == null) return null;
                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);
                if (photo == null) return null;
                if (photo.IsMain) return Result<Unit>.Failure("System can not delete main photo");
                var result = await _photoAccessor.DeletePhoto(photo.Id);
                if (result == null) return Result<Unit>.Failure("Error while deleting photo on cloud");
                user.Photos.Remove(photo);
                var success = await _transactionRepository.SaveChanges();
                return success
                    ? Result<Unit>.Success(Unit.Value)
                    : Result<Unit>.Failure("Error while deleting photo on database");
            }
        }
    }
}