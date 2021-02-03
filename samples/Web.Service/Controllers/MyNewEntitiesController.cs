using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlatformFramework.Eventing;
using PlatformFramework.Models;
using Web.Service.BusinessLogic;

namespace Web.Service.Controllers
{
    [Route("api/[controller]")]
    public class MyNewEntitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly MyEntityService _myEntityService;

        public MyNewEntitiesController(IMediator mediator, MyEntityService myEntityService)
        {
            _mediator = mediator;
            _myEntityService = myEntityService;
        }

        [HttpPost("[action]")]
        public Task<IEnumerable<MyEntityModel>> GetAll([FromBody] GetAllRequest request)
        {
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<PagedCollection<MyEntityModel>> GetAllPaged([FromBody] GetAllPagedRequest request)
        {
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Create([FromBody] MyEntityModel model)
        {
            return _myEntityService.Create(model);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Update([FromQuery] int id, [FromBody] MyEntityModel model)
        {
            var request = new UpdateRequest(id, model);
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Delete([FromQuery] int id)
        {
            var request = new DeleteRequest(id);
            return _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public Task<CustomResponse> Custom()
        {
            var request = new CustomRequest();
            return _mediator.Send(request);
        }
    }
}