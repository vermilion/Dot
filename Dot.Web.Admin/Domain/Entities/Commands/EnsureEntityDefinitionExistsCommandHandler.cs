using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    public class EnsureEntityDefinitionExistsCommandHandler 
        : IRequestHandler<EnsureEntityDefinitionExistsCommand, Unit>
    {
        #region constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityDefinitionRepository _entityDefinitionRepository;

        public EnsureEntityDefinitionExistsCommandHandler(
            IUnitOfWork unitOfWork,
            IEntityDefinitionRepository entityDefinitionRepository
            )
        {
            _unitOfWork = unitOfWork;
            _entityDefinitionRepository = entityDefinitionRepository;
        }

        #endregion

        #region Execute

        public async Task<Unit> ExecuteAsync(EnsureEntityDefinitionExistsCommand command, IExecutionContext executionContext)
        {
            var entityDefinition = _entityDefinitionRepository.GetByCode(command.EntityDefinitionCode);

            var dbDefinition = await _unitOfWork
                .EntityDefinitions()
                .SingleOrDefaultAsync(e => e.EntityDefinitionCode == command.EntityDefinitionCode);

            if (dbDefinition == null)
            {
                dbDefinition = new EntityDefinition
                {
                    EntityDefinitionCode = entityDefinition.EntityDefinitionCode,
                    Name = entityDefinition.Name
                };

                _unitOfWork.EntityDefinitions().Add(dbDefinition);
                await _unitOfWork.SaveChangesAsync();
            }

            return Unit.Value;
        }

        #endregion
    }
}
