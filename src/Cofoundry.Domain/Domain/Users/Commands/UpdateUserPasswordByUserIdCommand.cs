using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    public class UpdateUserPasswordByUserIdCommand : ICommand, ILoggableCommand
    {
        [Required]
        [PositiveInteger]
        public int UserId { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [IgnoreDataMember]
        [JsonIgnore]
        public string NewPassword { get; set; }
    }
}
