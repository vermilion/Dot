using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Service.BusinessLogic;

namespace Web.Service.Controllers
{
    [Route("api/[controller]")]
    public class MyNewEntitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MyNewEntitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> CreateUseCase([FromBody] CreateRequest request)
        {
            return _mediator.Send(request);
        }

        [HttpGet("[action]")]
        public Task<IEnumerable<MyEntityModel>> GetAllUseCase([FromBody] GetAllRequest request)
        {
            return _mediator.Send(request);
        }
    }
}