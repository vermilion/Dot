using PlatformFramework.EFCore.Domain;

namespace Web.Service
{
    public class MyEntity : Entity
    {
        public string? Title { get; set; }

        public string? SecondProperty { get; set; }
    }
}
