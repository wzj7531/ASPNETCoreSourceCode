using System.Collections.Generic;

namespace Microsoft.Extensions.FileProviders
{
    public interface IDirectoryContents : IEnumerable<IFileInfo>
    {
        /// <summary>
        /// True if a directory was located at the given path.
        /// </summary>
        bool Exists {get;}
    }    
}