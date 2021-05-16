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

namespace Finance.Application.Commands.PhotoCommands
{
    public class PhotoDelete
    {
           public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
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

            
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetUserPhoto();
                if (user == null) return null;
                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);
                if (photo == null) return null;
                if (photo.IsMain) return Result<Unit>.Failure("System can not delete main photo");
                var result = await _photoAccessor.DeletePhoto(photo.Id);
                if(result==null) return Result<Unit>.Failure("Error while deleting photo on cloud");
                user.Photos.Remove(photo);
                var success = await _transactionRepository.SaveChanges();
                if(success) return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Error while deleting photo on database");
            }
        }
    }
}