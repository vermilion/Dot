using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.Extensions;
using PlatformFramework.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Service.BusinessLogic
{
    public class MyEntityService : IMyEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityService<MyEntity> _service;

        public MyEntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _service = _unitOfWork.GetEntityServiceFor<MyEntity>();
        }

        public IQueryable<MyEntityModel> GetAll()
        {
            var result = _service.GetAll();
            return result.Project<MyEntityModel>();
        }

        public Task<PagedCollection<MyEntityModel>> GetAllPaged(int? offset, int? limit)
        {
            return _service.GetAllPaged<MyEntityModel>(offset, limit);
        }

        public async Task<MyEntityModel> Create(MyEntityModel model)
        {
            var entity = model.MapTo<MyEntity>();

            var result = await _service.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return result.MapTo<MyEntityModel>();
        }

        public async Task<MyEntityModel> Update(MyEntityModel model)
        {
            var entity = model.MapTo<MyEntity>();
            _service.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return entity.MapTo<MyEntityModel>();
        }

        public async Task Delete(int id)
        {
            await _service.Delete(new object[] { id });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
