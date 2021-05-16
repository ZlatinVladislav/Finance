using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.Interfaces;
using Finance.Application.Validations;
using Finance.Domain.Interfaces;
using Finance.Domain.Interfaces.Base;
using Finance.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Commands.PhotoCommands
{
    public class PhotoAdd
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
            private readonly IBaseRepository<Domain.Models.Transaction> _transactionRepository;
            private readonly IBaseRepository<TransactionType> _tansactionTypeRepository;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Domain.Models.Transaction> genericRepository,
                IBaseRepository<TransactionType> tansactionTypeRepository, IMapper mapper,
                IAppUserRepository appUserRepository,IPhotoAccessor photoAccessor)
            {
                _transactionRepository = genericRepository;
                _tansactionTypeRepository = tansactionTypeRepository;
                _mapper = mapper;
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
                
                if(result) return Result<Photo>.Success(photo);
                return Result<Photo>.Failure("Problem while adding photo");
            }
        }
    }
}