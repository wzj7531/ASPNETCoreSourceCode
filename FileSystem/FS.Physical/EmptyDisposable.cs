using System;

namespace Microsoft.Extensions.FileProviders
{
    internal class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Instance {get;} = new EmptyDisposable();

        private EmptyDisposable()
        {

        }

        public void Dispose()
        {

        }
    }
}