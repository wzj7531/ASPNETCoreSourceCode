using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Microsoft.Extensions.FileProviders.Physical
{
    /// <summary>
    /// <para>
    /// A file watcher that watches a physical filesystem for changes.
    /// </para>
    /// <para>
    /// Triggers events on <see cref="IChangeToken"/> when files are created, change, renamed,or deleted.
    /// </para>
    /// </summary>
    public class PhysicalFilesWatcher : IDisposable
    {
        private static readonly Action<object> _cancelTokenSource = state => ((CancellationTokenSource)state).Cancel();
        internal static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(4);

        private readonly ConcurrentDictionary<string,ChangeTokenInfo> _filePathTokenLookup = 
            new ConcurrentDictionary<string, ChangeTokenInfo>(StringComparer.OrdinalIgnoreCase);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private  struct ChangeTokenInfo
        {
            public ChangeTokenInfo(
                CancellationTokenSource tokenSource,
                CancellationChangeToken changeToken)
                :this(tokenSource,changeToken,matcher:null)
            {

            }

            public ChangeTokenInfo(CancellationTokenSource tokenSource,CancellationChangeToken changeToken,Matcher matcher)
            {
                TokenSource = tokenSource;
                ChangeToken = changeToken;
                Matcher = matcher;
            }

            public CancellationTokenSource TokenSource{ get;}

            public CancellationChangeToken ChangeToken{ get;}

            public Matcher Matcher{get;}
        }
    }
}