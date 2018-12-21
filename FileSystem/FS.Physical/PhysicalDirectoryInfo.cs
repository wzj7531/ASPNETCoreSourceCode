using System;
using System.IO;

namespace Microsoft.Extensions.FileProviders.Physical
{
    public class PhysicalDirectoryInfo:IFileInfo
    {
        private readonly DirectoryInfo _info;

        /// <summary>
        /// Initializes an instance of <see cref="PysicalDirectoryInfo"/> that wraps an instance of <see cref="System.IO.Directory"/>
        /// </summary>
        /// <param name="info"></param>
        public PhysicalDirectoryInfo(DirectoryInfo info)
        {
            _info = info;
        }

        ///<inhericdoc />
        public bool Exists => _info.Exists;

        /// <summary>
        /// Always equals -1
        /// </summary>
        public long Length => -1;


        /// <inheritdoc />
        public string PhysicalPath => _info.FullName;

        ///<inheritdoc />
        public string Name => _info.Name;

        /// <summary>
        /// The time when the directory was last written to.
        /// </summary>
        public DateTimeOffset LastModified => _info.LastWriteTimeUtc;

        /// <summary>
        /// Always true
        /// </summary>
        public bool IsDirectory => true;

        public Stream CreateReadStream()
        {
            throw new InvalidOperationException("Can't create a stream for a directory.");
        }
    }
}