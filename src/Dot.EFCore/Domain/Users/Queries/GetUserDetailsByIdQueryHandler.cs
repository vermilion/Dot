﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Finds a user by a database id returning a UserDetails object if it 
    /// is found, otherwise null.
    /// </summary>
    public class GetUserDetailsByIdQueryHandler 
        : IRequestHandler<GetUserDetailsByIdQuery, UserDetails>
    {
        private readonly DbContextCore _dbContext;
        private readonly IPermissionValidationService _permissionValidationService;
        private readonly IUserDetailsMapper _userDetailsMapper;

        public GetUserDetailsByIdQueryHandler(
            DbContextCore dbContext,
            IPermissionValidationService permissionValidationService,
            IUserDetailsMapper userDetailsMapper
            )
        {
            _dbContext = dbContext;
            _permissionValidationService = permissionValidationService;
            _userDetailsMapper = userDetailsMapper;
        }

        public async Task<UserDetails> ExecuteAsync(GetUserDetailsByIdQuery query, IExecutionContext executionContext)
        {
            var dbUser = await _dbContext
                .Users
                .AsNoTracking()
                .Include(u => u.Creator)
                .Include(u => u.Role)
                .Where(u => u.UserId == query.UserId)
                .SingleOrDefaultAsync();

            var user = _userDetailsMapper.Map(dbUser);

            if (user != null)
            {
                _permissionValidationService.EnforceCurrentUserOrHasPermission<CofoundryUserReadPermission>(query.UserId, executionContext.UserContext);
            }

            return user;
        }
    }
}
