using Cofoundry.Domain.Data;
using Ardalis.Specification;

namespace Cofoundry.Domain.Internal
{
    public class UserByIdSpec : Specification<User>
    {
        public UserByIdSpec(int? Id)
        {
            Query
                .FilterById(Id)
                .FilterCanLogIn();
        }
    }
}
