using PlatformFramework.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Service.BusinessLogic
{
    public interface IMyEntityService
    {
        Task<MyEntityModel> Create(MyEntityModel model);
        Task Delete(int id);
        IQueryable<MyEntityModel> GetAll();
        Task<PagedCollection<MyEntityModel>> GetAllPaged(int? offset, int? limit);
        Task<MyEntityModel> Update(MyEntityModel model);
    }
}