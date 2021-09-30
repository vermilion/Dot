using Cofoundry.Domain;

namespace Dot.Web.Admin.Domain.Setup.Models
{
    public class InternalSettings : ICofoundrySettings
    {
        /// <summary>
        /// Indicates whether the site setup has been completed successfully.
        /// </summary>
        public bool IsSetup { get; set; }
    }
}
