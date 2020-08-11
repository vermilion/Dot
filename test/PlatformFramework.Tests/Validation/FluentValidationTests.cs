using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Validation;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PlatformFramework.Tests.Validation
{
    public class FluentValidationTests
    {
        [Fact]
        public void Should_Have_Failures_When_Validator_Resolved_And_Defined_Some_Rules()
        {
            var services = new ServiceCollection();
            services
                .AddFramework(x =>
                {
                    x.Assemblies.Clear();
                    x.Assemblies.Add(Assembly.GetExecutingAssembly());
                })
                .WithValidation();

            services.AddSingleton<IValidatorFactory, ServiceProviderValidatorFactory>();

            var factory = services.BuildServiceProvider().GetRequiredService<IValidatorFactory>();
            var validator = factory.GetValidator<TestModel>();

            Assert.NotNull(validator);
            var result = validator.Validate(new TestModel());

            Assert.False(result.IsValid);
            var failureResults = result.Errors.Any(result => result.ErrorMessage == nameof(TestModelValidator));
            Assert.True(failureResults);
        }

        public class TestModel
        {
        }

        public class TestModelValidator : AbstractValidator<TestModel>
        {
            public TestModelValidator()
            {
                RuleFor(m => m)
                    .Custom((model, context) =>
                    {
                        context.AddFailure(nameof(TestModelValidator));
                    });
            }
        }
    }
}
