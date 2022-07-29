using System.Collections.Generic;

namespace Cofoundry.Domain
{
    public interface IEntityDefinitionRepository
    {
        IEntityDefinition? GetByCode(string code);

        IEnumerable<IEntityDefinition> GetAll();
    }
}
