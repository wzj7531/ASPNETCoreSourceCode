using System;

namespace Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments
{
    public class ParentPathSegment : IPathSegment
    {
        private static readonly string LiteralParent = "..";
        public bool CanProduceStem {get {return false;}}

        public bool Match(string valaue)
        {
            return string.Equals(LiteralParent,valaue,StringComparison.Ordinal);
        }
    }
}