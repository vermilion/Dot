using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Domain
{
    public class UpdateUnstructuredDataDependenciesCommand : IRequest<Unit>
    {
        public UpdateUnstructuredDataDependenciesCommand(string rootEntityDefinitionCode, int rootEntityId, object model)
        {
            RootEntityDefinitionCode = rootEntityDefinitionCode;
            RootEntityId = rootEntityId;
            Model = model;
        }

        [Required]
        public string RootEntityDefinitionCode { get; set; }

        [Required]
        [PositiveInteger]
        public int RootEntityId { get; set; }

        [Required]
        public object Model { get; set; }
    }
}
