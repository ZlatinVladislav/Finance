using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Interfaces;
using Finance.Application.Validations;
using Finance.Domain.Models;
using Finance.Infrastructure.Data.Interfaces;
using Finance.Infrastructure.Data.Interfaces.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.CQRS.Commands.PhotoCommands
{
    public class PhotoAdd
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
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


            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUserPhoto();
                if (user == null) return null;
                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);
                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

                user.Photos.Add(photo);

                var result = await _transactionRepository.SaveChanges();

                return result ? Result<Photo>.Success(photo) : Result<Photo>.Failure("Problem while adding photo");
            }
        }
    }
}