using System;
using System.Threading;

namespace Microsoft.Extensions.Primitives
{
    /// <summary>
    /// A <see cref="IChangeToken"/> implementation using <see cref="CancellationToken"/>
    /// </summary>
    public class CancellationChangeToken : IChangeToken
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CancellationChangeToken"/>.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        public CancellationChangeToken(CancellationToken cancellationToken)
        {
            Token = cancellationToken;
        }

        /// <inheritdoc />
        public bool HasChanged => Token.IsCancellationRequested;

        /// <inheritdoc />
        public bool ActiveChangeCallbacks {get; private set;} = true;

        private CancellationToken Token { get; }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            var restoreFlow = false;
            if(!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
                restoreFlow = true;
            }

            try
            {
                return Token.Register(callback,state);
            }
            catch (ObjectDisposedException)
            {
                
                ActiveChangeCallbacks = false;
            }
            finally
            {
                if(restoreFlow)
                {
                    ExecutionContext.RestoreFlow();
                }
            }
            return NullDisposable.Instance;
        }

        private class NullDisposable : IDisposable
        {
            public static readonly NullDisposable Instance = new NullDisposable();
            public void Dispose()
            {

            }
        }
    }
}