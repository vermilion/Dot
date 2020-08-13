using Ardalis.GuardClauses;

namespace PlatformFramework.Validation
{
    public class ValidationError
    {
        public ValidationError(string memberName, string message)
        {
            MemberName = Guard.Against.NullOrEmpty(memberName, nameof(memberName));
            Message = Guard.Against.NullOrEmpty(message, nameof(message));
        }

        public string MemberName { get; }
        public string Message { get; }

        public override string ToString()
        {
            return $"[{MemberName}: {Message}]";
        }
    }
}