using System.ComponentModel.DataAnnotations;
using Cofoundry.Core.Configuration;

namespace Cofoundry.BasicTestSite
{
    public class BasicTestSiteSettings : IConfigurationSettings
    {
        /// <summary>
        /// Setting Name = SimpleTestSite:ContactRequestNotificationToAddress
        /// </summary>
        [Required]
        [EmailAddress]
        public string ContactRequestNotificationToAddress { get; set; }
    }
}