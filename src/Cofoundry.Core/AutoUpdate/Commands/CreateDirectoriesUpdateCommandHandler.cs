using System.IO;
using System.Threading.Tasks;

namespace Cofoundry.Core.AutoUpdate
{
    public class CreateDirectoriesUpdateCommandHandler : IVersionedUpdateCommandHandler<CreateDirectoriesUpdateCommand>
    {
        private IPathResolver _pathResolver;

        public CreateDirectoriesUpdateCommandHandler(
            IPathResolver pathResolver
            )
        {
            _pathResolver = pathResolver;
        }
        
        public async Task ExecuteAsync(CreateDirectoriesUpdateCommand command)
        {
            foreach (var path in command.Directories)
            {
                CreateDirectory(path);
            }
        }

        private void CreateDirectory(string directory)
        {
            var path = _pathResolver.MapPath(directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
