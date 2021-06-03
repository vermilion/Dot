using Cofoundry.Core;
using System.Collections.Generic;
using System.Linq;

namespace Cofoundry.Domain.Internal
{
    public class EntityDefinitionRepository : IEntityDefinitionRepository
    {
        #region constructor

        private readonly Dictionary<string, IEntityDefinition> _entityDefinitions;

        public EntityDefinitionRepository(
            IEnumerable<IEntityDefinition> allDefinitions
            )
        {
            DetectInvalidDefinitions(allDefinitions);

            _entityDefinitions = allDefinitions.ToDictionary(k => k.EntityDefinitionCode);
        }

        private void DetectInvalidDefinitions(IEnumerable<IEntityDefinition> definitions)
        {
            var nullCode = definitions
                .Where(d => string.IsNullOrWhiteSpace(d.EntityDefinitionCode))
                .FirstOrDefault();

            if (nullCode != null)
            {
                var message = nullCode.GetType().Name + " does not have a definition code specified.";
                throw new InvalidEntityDefinitionException(message, nullCode, definitions);
            }

            var nullName = definitions
                .Where(d => string.IsNullOrWhiteSpace(d.Name))
                .FirstOrDefault();

            if (nullName != null)
            {
                var message = nullName.GetType().Name + " does not have a name specified.";
                throw new InvalidEntityDefinitionException(message, nullName, definitions);
            }

            var dulpicateCode = definitions
                .GroupBy(e => e.EntityDefinitionCode)
                .Where(g => g.Count() > 1)
                .FirstOrDefault();

            if (dulpicateCode != null)
            {
                var message = "Duplicate IEntityDefinition.EntityDefinitionCode: " + dulpicateCode.Key;
                throw new InvalidEntityDefinitionException(message, dulpicateCode.First(), definitions);
            }

            var dulpicateName = definitions
                .GroupBy(e => e.Name)
                .Where(g => g.Count() > 1)
                .FirstOrDefault();

            if (dulpicateName != null)
            {
                var message = "Duplicate IEntityDefinition.Name: " + dulpicateName.Key;
                throw new InvalidEntityDefinitionException(message, dulpicateName.First(), definitions);
            }
        }

        #endregion

        public IEntityDefinition GetByCode(string code)
        {
            return _entityDefinitions.GetOrDefault(code);
        }

        public IEnumerable<IEntityDefinition> GetAll()
        {
            return _entityDefinitions.Values;
        }
    }
}
