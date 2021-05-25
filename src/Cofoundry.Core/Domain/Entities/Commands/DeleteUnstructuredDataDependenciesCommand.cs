using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Domain
{
    public class DeleteUnstructuredDataDependenciesCommand : IRequest<Unit>
    {
        public DeleteUnstructuredDataDependenciesCommand(string rootEntityDefinitionCode, int rootEntityId)
        {
            RootEntityDefinitionCode = rootEntityDefinitionCode;
            RootEntityId = rootEntityId;
        }

        [Required]
        public string RootEntityDefinitionCode { get; set; }

        [Required]
        [PositiveInteger]
        public int RootEntityId { get; set; }
    }
}
