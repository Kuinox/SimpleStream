using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStream.AsyncExtensions
{
    public static class ComputedPositionAsyncExtensions
    {
        public static IAsyncReadStreamKnownPosition ComputePosition( this IAsyncReadStream @this )
            => new ComputePositionStream( @this );
        class ComputePositionStream : IAsyncReadStreamKnownPosition
        {
            readonly IAsyncReadStream _stream;

            public ComputePositionStream( IAsyncReadStream stream )
            {
                _stream = stream;
            }

            public long Position { get; private set; }

            public async ValueTask<int> ReadAsync( Memory<byte> buffer )
            {
                int readAmount = await _stream.ReadAsync( buffer );
                Position += readAmount;
                return readAmount;
            }
        }
    }
}
