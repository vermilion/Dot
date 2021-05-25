﻿using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cofoundry.Domain
{
   public class GetSettingsQuery<TEntity>
        : IRequest<TEntity>
        where TEntity : ICofoundrySettings
    {
    }
}
