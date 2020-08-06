using MediatR;

namespace PlatformFramework.UseCases
{
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithMediatr(this FrameworkBuilder builder)
        {
            var services = builder.Services;
            var options = builder.Options;

            //var config = builder.GetOption<FrameworkMediatrOptions>();

            services.AddMediatR(options.Assemblies, x => x.AsTransient());

            return builder;
        }
    }
}