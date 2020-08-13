using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlatformFramework.Models;
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
        public Task<IEnumerable<MyEntityModel>> GetAll([FromBody] GetAllRequest request)
        {
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<PagedModel<MyEntityModel>> GetAllPaged([FromBody] GetAllPagedRequest request)
        {
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Create([FromBody] MyEntityModel model)
        {
            var request = new CreateRequest(model);
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Update([FromQuery] long id, [FromBody] MyEntityModel model)
        {
            var request = new UpdateRequest(id, model);
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Delete([FromQuery] long id)
        {
            var request = new DeleteRequest(id);
            return _mediator.Send(request);
        }
    }
}