using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Microsoft.Extensions.FileSystemGlobbing
{
    public class Matcher
    {
        private readonly IList<IPattern> _includePatterns = new List<IPattern>();
        private readonly IList<IPattern> _excludePatterns = new List<IPattern>();
    }
}
