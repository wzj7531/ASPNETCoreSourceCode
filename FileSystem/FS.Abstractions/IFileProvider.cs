using Microsoft.Extensions.Primitives;

namespace Microsoft.Extensions.FileProviders
{
    public interface IFileProvider
    {
        /// <summary>
        /// Locate a file at the given path
        /// </summary>
        /// <param name="subPath">Relative path that identifies the file.</param>
        /// <returns>The file information. Caller must check Exists property.</returns>
        IFileInfo GetFileInfo(string subPath);

        /// <summary>
        /// Enumerate a directory at the given path, if any.
        /// </summary>
        /// <param name="subPath">Relative path that identifies the directory.</param>
        /// <returns>Returns the contents of the directory.</returns>
        IDirectoryContents GetDirectoryContents(string subPath);

        /// <summary>
        /// Creates a <see cref="IChangeToken"/> for the specified <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">Filter string used to determine what files or folder to monitor. Example:**/*.cs, *.*,subFolder/**/*.cshtml</param>
        /// <returns>An <see cref="IChangeToken"/> that is notified when a file matching <paramref name="filter"></returns>
        IChangeToken Watch(string filter);
    }
}