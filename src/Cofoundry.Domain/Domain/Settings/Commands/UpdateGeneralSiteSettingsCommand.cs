﻿using Cofoundry.Domain.CQS;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Domain
{
    public class UpdateGeneralSiteSettingsCommand : IRequest<Unit>, ILoggableCommand
    {
        [Required]
        [MaxLength(100)]
        public string ApplicationName { get; set; }

        public bool AllowAutomaticUpdates { get; set; }
    }
}
