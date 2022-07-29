using System.ComponentModel.DataAnnotations;
using Cofoundry.Core.Configuration;

namespace Cofoundry.Core
{
    /// <summary>
    /// Settings to use when connecting to the Cofoundry database.
    /// </summary>
    public class DatabaseSettings : DotConfigurationSettingsBase
    {
        /// <summary>
        /// The connection string to the Cofoundry database.
        /// </summary>
        [Required]
        public string ConnectionString { get; set; }
    }
}
