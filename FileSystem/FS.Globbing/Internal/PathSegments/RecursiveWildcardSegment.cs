namespace Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments
{
    public class RecursiveWildcardSegment : IPathSegment
    {
        public bool CanProduceStem {get {return true;} }

        public bool Match(string valaue)
        {
            return false;
        }
    }
}