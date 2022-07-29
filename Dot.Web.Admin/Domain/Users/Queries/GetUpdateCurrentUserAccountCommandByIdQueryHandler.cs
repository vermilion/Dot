using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    public class GetUpdateCurrentUserAccountCommandByIdQueryHandler 
        : IRequestHandler<GetUpdateCommandByIdQuery<UpdateCurrentUserAccountCommand>, UpdateCurrentUserAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionValidationService _permissionValidationService;

        public GetUpdateCurrentUserAccountCommandByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IPermissionValidationService permissionValidationService
            )
        {
            _unitOfWork = unitOfWork;
            _permissionValidationService = permissionValidationService;
        }

        public async Task<UpdateCurrentUserAccountCommand> ExecuteAsync(GetUpdateCommandByIdQuery<UpdateCurrentUserAccountCommand> query, IExecutionContext executionContext)
        {
            if (!executionContext.UserContext.UserId.HasValue) return null;

            var user = await _unitOfWork
                .Users()
                .AsNoTracking()
                .FilterCanLogIn()
                .FilterById(executionContext.UserContext.UserId.Value)
                .Select(u => new UpdateCurrentUserAccountCommand()
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .SingleOrDefaultAsync();

            return user;
        }
    }
}
