using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.FileProviders
{
    public class NotFoundDirectoryContents : IDirectoryContents
    {
        /// <summary>
        /// A shared instance of <see cref="NotFoundDirectoryContents">
        /// </summary>
        public static NotFoundDirectoryContents Singleton {get;} = new NotFoundDirectoryContents();

        /// <summary>
        /// Always false.
        /// </summary>
        public bool Exists => false;

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator to an empty collection</returns>
        public IEnumerator<IFileInfo> GetEnumerator()
        {
            return Enumerable.Empty<IFileInfo>().GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}