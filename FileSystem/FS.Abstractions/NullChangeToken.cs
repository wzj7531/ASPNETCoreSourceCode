using System;
using Microsoft.Extensions.Primitives;

namespace Microsoft.Extensions.FileProviders
{   
    public class NullChangeToken : IChangeToken
    {
        /// <summary>
        /// A signleton instance of <see cref="NullChangeToken"/>
        /// </summary>
        public static NullChangeToken Singleton { get; } = new NullChangeToken();

        private NullChangeToken()
        {

        }

        /// <summary>
        /// Always false.
        /// </summary>
        public bool HasChanged => false;

        /// <summary>
        /// Always false.
        /// </summary>
        public bool ActiveChangeCallbacks => false;

        /// <summary>
        /// Always returns an empty disposable object. Callback will never be called.
        /// </summary>
        /// <param name="callback">This parameter is ignored</param>
        /// <param name="state">This parameter is ignored</param>
        /// <returns>A disposable object that noops on dispose.</returns>
        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            return EmptyDisposable.Instance;
        }
    }
}