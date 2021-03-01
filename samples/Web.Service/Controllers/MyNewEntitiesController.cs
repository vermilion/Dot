using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlatformFramework.Models;
using Web.Service.BusinessLogic;

namespace Web.Service.Controllers
{
    [Route("api/[controller]")]
    public class MyNewEntitiesController : ControllerBase
    {
        private readonly IMyEntityService _service;

        public MyNewEntitiesController(IMyEntityService myEntityService)
        {
            _service = myEntityService;
        }

        [HttpPost("[action]")]
        public Task<List<MyEntityModel>> GetAll()
        {
            return Task.FromResult(_service.GetAll().ToList());
        }

        [HttpPost("[action]")]
        public Task<PagedCollection<MyEntityModel>> GetAllPaged([FromQuery] int offset, [FromQuery] int limit)
        {
            return _service.GetAllPaged(offset, limit);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Create([FromBody] MyEntityModel model)
        {
            return _service.Create(model);
        }

        [HttpPost("[action]")]
        public Task<MyEntityModel> Update([FromBody] MyEntityModel model)
        {
            return _service.Update(model);
        }

        [HttpPost("[action]")]
        public Task Delete([FromQuery] int id)
        {
            return _service.Delete(id);
        }
    }
}