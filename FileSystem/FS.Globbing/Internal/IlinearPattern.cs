using System.Collections.Generic;

namespace Microsoft.Extensions.FileSystemGlobbing.Internal
{
    public interface IlinearPattern : IPattern
    {
        IList<IPathSegment> Segments{get;}
    }
}