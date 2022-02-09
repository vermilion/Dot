using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Simple mapper for mapping to UserAccountDetails objects.
    /// </summary>
    public class UserAccountDetailsMapper : IUserAccountDetailsMapper
    {
        private readonly IUserMicroSummaryMapper _userMicroSummaryMapper;

        public UserAccountDetailsMapper(
            IUserMicroSummaryMapper userMicroSummaryMapper
            )
        {
            _userMicroSummaryMapper = userMicroSummaryMapper;
        }

        /// <summary>
        /// Maps an EF user record from the db into a UserAccountDetails object. If the
        /// db record is null then null is returned.
        /// </summary>
        /// <param name="dbUser">User record from the database.</param>
        public virtual UserAccountDetails Map(User dbUser)
        {
            if (dbUser == null) return null;

            var user = new UserAccountDetails()
            {
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                UserId = dbUser.UserId,
                Username = dbUser.Username,
                LastLoginDate = DbDateTimeMapper.AsUtc(dbUser.LastLoginDate),
                LastPasswordChangeDate = DbDateTimeMapper.AsUtc(dbUser.LastPasswordChangeDate),
                PreviousLoginDate = DbDateTimeMapper.AsUtc(dbUser.PreviousLoginDate),
                RequirePasswordChange = dbUser.RequirePasswordChange
            };

            user.AuditData = new CreateAuditData()
            {
                CreateDate = DbDateTimeMapper.AsUtc(dbUser.CreateDate)
            };

            if (dbUser.Creator != null)
            {
                user.AuditData.Creator = _userMicroSummaryMapper.Map(dbUser.Creator);
            }

            return user;
        }
    }
}
