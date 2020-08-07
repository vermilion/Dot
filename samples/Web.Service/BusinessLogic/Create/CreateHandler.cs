using AutoMapper;
using MediatR;
using PlatformFramework.EFCore.Context;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Service.BusinessLogic
{
    public class CreateHandler : IRequestHandler<CreateRequest, MyEntityModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MyEntityModel> Handle(CreateRequest model, CancellationToken cancellationToken = default)
        {
            var dbSet = _unitOfWork.Set<MyEntity>();

            var entity = _mapper.Map<MyEntity>(model);

            await dbSet.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<MyEntityModel>(entity);
            return dto;
        }
    }
}
