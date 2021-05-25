using Cofoundry.Core.Caching;
using Cofoundry.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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