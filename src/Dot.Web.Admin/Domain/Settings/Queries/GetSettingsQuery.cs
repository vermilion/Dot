using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    public class GetSettingsQuery<TEntity> : IRequest<TEntity>
         where TEntity : ICofoundrySettings
    {
    }
}
