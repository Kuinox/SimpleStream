using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleStream.Async.Extensions
{
    public static class FromRegularStreamExtensions
    {
        public static IReadStream GetSimpleReadStream( this Stream @this)
        {
            if( !@this.CanRead ) throw new ArgumentException( "Cannot get a Simple Read Stream out of the given stream, because CanRead is false." );
            return new StreamAdaptator( @this );
        }

        class StreamAdaptator : IReadStream
        {
            readonly Stream _stream;

            public StreamAdaptator(Stream stream)
            {
                _stream = stream;
            }

            public int Read( Span<byte> buffer ) => _stream.Read( buffer );
        }
    }
}
