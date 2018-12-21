namespace Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments
{
    public class CurrentPathSegment : IPathSegment
    {
        public bool CanProduceStem {get{return false;}}

        public bool Match(string valaue)
        {
            return false;
        }
    }
}