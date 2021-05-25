﻿using System;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Simple audit data for entity creations.
    /// </summary>
    public class CreateAuditData
    {
        /// <summary>
        /// The user that created the entity.
        /// </summary>
        public UserMicroSummary Creator { get; set; }

        /// <summary>
        /// The date the entity was created.
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
