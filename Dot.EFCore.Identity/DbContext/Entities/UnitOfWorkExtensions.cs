using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Data
{
    public static class EntityDefinitionUnitOfWorkExtensions
    {
        public static DbSet<EntityDefinition> EntityDefinitions(this IUnitOfWork unitOfWork)
        {
            return unitOfWork.Set<EntityDefinition>();
        }
    }
}
