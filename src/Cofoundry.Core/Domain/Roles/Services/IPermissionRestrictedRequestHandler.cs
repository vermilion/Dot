using Cofoundry.Domain.CQS;
using System.Collections.Generic;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Used to ensure a user has a specific set of permissions before the handler is executed
    /// </summary>
    public interface IPermissionRestrictedRequestHandler : IPermissionCheckHandler
    {
    }

    /// <summary>
    /// Used to ensure a user has a specific set of permissions before the handler is executed
    /// </summary>
    public interface IPermissionRestrictedRequestHandler<TRequest> : IPermissionRestrictedRequestHandler 
        where TRequest : IRequest
    {
        IEnumerable<IPermissionApplication> GetPermissions(TRequest command);
    }
}
