using PlatformFramework.Validation;
using System.Collections.Generic;

namespace PlatformFramework.Results
{
    public interface IResult
    {
        ResultStatus Status { get; }
        IEnumerable<string> Errors { get; }
        List<ValidationError> ValidationErrors { get; }
    }
}
