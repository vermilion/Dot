﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Core.Web
{
    /// <summary>
    /// Service for working with mime types. The default implementation 
    /// gets mime types from IContentTypeProvider underneath. You can
    /// add additional mime type mappings by implementing IMimeTypeRegistration
    /// which automatically adds the additional mime types at startup.
    /// </summary>
    public interface IMimeTypeService
    {
        /// <summary>
        /// Finds a mime type that matches the file extension in a file name. Equivalent to 
        /// the old MimeMapping.GetMimeMapping method from .NET 4.x.
        /// </summary>
        /// <param name="fileName">File name with file extension (path optional).</param>
        string MapFromFileName(string fileName);

        /// <summary>
        /// Finds a mime type that matches the file extension in a file name.
        /// If a matching mime type is not found then the specified default
        /// mime type is returned, or if the default value is null or empty
        /// then "application/octet-stream" is used instead.
        /// </summary>
        /// <param name="fileName">File name with file extension (path optional).</param>
        /// <param name="defaultMimeType">
        /// The default mime type to use if a match cannot be found. If this
        /// default value is null or empty then "application/octet-stream" 
        /// is used instead.
        /// </param>
        string MapFromFileName(string fileName, string defaultMimeType);
    }
}