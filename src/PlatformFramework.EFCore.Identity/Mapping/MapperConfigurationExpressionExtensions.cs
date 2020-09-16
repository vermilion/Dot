using AutoMapper.Configuration;

namespace PlatformFramework.EFCore.Identity.Mapping
{
    public static class MapperConfigurationExpressionExtensions
    {
        public static MapperConfigurationExpression AddIdentityMappingProfiles(this MapperConfigurationExpression expression)
        {
            expression.AddProfile<UserMappingProfile>();
            expression.AddProfile<RoleMappingProfile>();

            return expression;
        }
    }
}
