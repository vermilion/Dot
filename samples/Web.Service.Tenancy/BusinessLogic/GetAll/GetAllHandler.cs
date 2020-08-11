using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Service.BusinessLogic
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, IEnumerable<MyEntityModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MyEntityModel>> Handle(GetAllRequest model, CancellationToken cancellationToken = default)
        {
            var query = _unitOfWork.Set<MyEntity>().AsNoTracking();

            var result = await _mapper.ProjectTo<MyEntityModel>(query).ToArrayAsync();
            return result.AsEnumerable();
        }
    }
}
