using Dot.Caching.Services;
using Dot.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class CacheApiController : BaseApiController
    {
        private readonly IObjectCacheFactory _objectCacheFactory;

        public CacheApiController(
            IObjectCacheFactory objectCacheFactory
            )
        {
            _objectCacheFactory = objectCacheFactory;
        }

        /// <summary>
        /// Admin remote access method to clear the
        /// data cache in case we run into a caching issue in a live deployment
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            _objectCacheFactory.Clear();

            return Ok(Enumerable.Empty<ValidationError>());
        }
    }
}