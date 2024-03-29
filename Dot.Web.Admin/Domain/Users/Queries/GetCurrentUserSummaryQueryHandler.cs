﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Gets a UserSummary object representing the currently logged in 
    /// user. If the user is not logged in then null is returned.
    /// </summary>
    public class GetCurrentUserSummaryQueryHandler
        : IRequestHandler<GetCurrentUserSummaryQuery, UserSummary>
    {
        private readonly IMediator _queryExecutor;

        public GetCurrentUserSummaryQueryHandler(
            IMediator queryExecutor
            )
        {
            _queryExecutor = queryExecutor;
        }

        public Task<UserSummary> ExecuteAsync(GetCurrentUserSummaryQuery query, IExecutionContext executionContext)
        {
            if (!executionContext.UserContext.UserId.HasValue) return null;

            var userQuery = new GetUserSummaryByIdQuery(executionContext.UserContext.UserId.Value);

            return _queryExecutor.ExecuteAsync(userQuery, executionContext);
        }
    }
}
