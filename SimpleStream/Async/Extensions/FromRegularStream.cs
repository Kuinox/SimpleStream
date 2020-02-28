using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStream.Async.AsyncExtensions
{
    public static class FromRegularStreamAsyncExtensions
    {
        public static IAsyncReadStream GetSimpleReadStream( this Stream @this)
        {
            if( !@this.CanRead ) throw new ArgumentException( "Cannot get a Simple Read Stream out of the given stream, because CanRead is false." );
            return new StreamAdaptator( @this );
        }

        class StreamAdaptator : IAsyncReadStream
        {
            readonly Stream _stream;

            public StreamAdaptator(Stream stream)
            {
                _stream = stream;
            }

            public ValueTask<int> ReadAsync( Memory<byte> buffer ) => _stream.ReadAsync( buffer );
        }
    }
}
