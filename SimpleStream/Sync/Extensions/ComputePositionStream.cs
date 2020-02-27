using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStream.Extensions
{
    public static class ComputedPositionExtensions
    {
        public static IReadStreamKnownPosition ComputePosition( this IReadStream @this )
            => new ComputePositionStream( @this );
        class ComputePositionStream : IReadStreamKnownPosition
        {
            readonly IReadStream _stream;

            public ComputePositionStream( IReadStream stream )
            {
                _stream = stream;
            }

            public long Position { get; private set; }

            public int Read( Span<byte> buffer )
            {
                int readAmount = _stream.Read( buffer );
                Position += readAmount;
                return readAmount;
            }
        }
    }
}
