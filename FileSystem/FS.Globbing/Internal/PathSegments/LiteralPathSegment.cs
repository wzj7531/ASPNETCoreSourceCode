using System;
using Microsoft.Extensions.FileSystemGlobbing.Util;

namespace Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments
{
    public class LiteralPathSegment : IPathSegment
    {
        private readonly StringComparison _comparisonType;

        public bool CanProduceStem{ get { return false; } }

        public LiteralPathSegment(string value, StringComparison comparisonType)
        {
            if(value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            Value = value;
            _comparisonType = comparisonType;
        }

        public string Value { get; }

        public bool Match(string valaue)
        {
            return string.Equals(Value,valaue,_comparisonType);
        }

        public override bool Equals(object obj)
        {
           var other = obj as LiteralPathSegment;

           return other != null && 
            _comparisonType == other._comparisonType && 
            string.Equals(other.Value,Value,_comparisonType);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
           return StringComparisonHelper.GetStringComparer(_comparisonType).GetHashCode(Value);
        }
    }
}