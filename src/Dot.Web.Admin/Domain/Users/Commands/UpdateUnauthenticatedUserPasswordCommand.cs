using Cofoundry.Domain.CQS;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Updates the password of an unathenticated user, using the
    /// credentials in the command to authenticate the request.
    /// </summary>
    public class UpdateUnauthenticatedUserPasswordCommand : IRequest<UpdateUnauthenticatedUserPasswordCommandResult>, ILoggableCommand
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [IgnoreDataMember]
        [JsonIgnore]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [IgnoreDataMember]
        [JsonIgnore]
        public string NewPassword { get; set; }
    }

    public class UpdateUnauthenticatedUserPasswordCommandResult
    {
        /// <summary>
        /// The database id of the updated user. This is set after the command
        /// has been run.
        /// </summary>
        public int UserId { get; set; }
    }
}
