using Microsoft.AspNetCore.Mvc;
using System;

namespace PlatformFramework.Results
{
    /// <summary>
    /// Extensions to support converting Result to an ActionResult
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Convert an Ardalis.Result to a Microsoft.AspNetCore.Mvc.ActionResult
        /// </summary>
        /// <typeparam name="T">The value being returned</typeparam>
        /// <param name="controller">The controller this is called from</param>
        /// <param name="result">The Result to convert to an ActionResult</param>
        /// <param name="handleResult">Custom result handler</param>
        /// <returns></returns>
        public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result, Func<Result<T>, IActionResult>? handleResult = null)
        {
            var res = handleResult?.Invoke(result);
            if (res != null)
                return res;

            if (result.Status == ResultStatus.NotFound)
                return controller.NotFound();

            if (result.Status == ResultStatus.Invalid)
                return controller.BadRequest(result.ValidationErrors);

            return controller.Ok(result.Value);
        }
    }
}
