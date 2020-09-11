using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.Eventing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Service.BusinessLogic
{
    public class CustomRequest : IRequest<CustomResponse>
    {
    }

    public class CustomResponse
    {
    }

    public class CustomHandler : EntityHandlerBase<CustomRequest, CustomResponse>
    {
        private readonly ICustomService _service;

        public CustomHandler(IServiceProvider serviceProvider, ICustomService service)
            : base(serviceProvider)
        {
            _service = service;
        }

        public override async Task<CustomResponse> Handle(CustomRequest request, CancellationToken cancellationToken)
        {
            return _service.Process(request);
        }
    }

    public interface ICustomService
    {
        CustomResponse Process(CustomRequest request);
    }

    public class CustomService : ICustomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CustomResponse Process(CustomRequest request)
        {
            return null;
        }
    }
}
