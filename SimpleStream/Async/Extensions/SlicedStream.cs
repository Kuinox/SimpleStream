using SimpleStream.AsyncExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStream.AsyncExtensions
{
    public static class SliceAsyncExtensions
    {
        public static IAsyncReadStreamKnownPositionAndLength Slice( IAsyncReadStream @this, long sliceLength )
            => new SlicedStream( @this.ComputePosition(), sliceLength );
        class SlicedStream : IAsyncReadStreamKnownPositionAndLength
        {
            readonly IAsyncReadStreamKnownPosition _stream;
            public SlicedStream( IAsyncReadStreamKnownPosition stream, long sliceLength )
            {
                _stream = stream;
                Length = sliceLength;
            }

            public long Length { get; }

            public long Position => _stream.Position;

            public async ValueTask<int> ReadAsync( Memory<byte> buffer )
            {
                int amount = buffer.Length;
                long finalPos = amount + Position;
                int amountToRead = finalPos > Length ?
                      (int)(Length - Position)
                    : amount;
                int amountRead = await _stream.ReadAsync( buffer.Slice( 0, amountToRead ) );
                Debug.Assert( Position <= Length );
                return amountRead;
            }
        }
    }
    
}
