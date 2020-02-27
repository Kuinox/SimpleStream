using SimpleStream.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SimpleStream.Extensions
{
    public static class SliceExtensions
    {
        public static IReadStreamKnownPositionAndLength Slice( IReadStream @this, long sliceLength )
            => new SlicedStream( @this.ComputePosition(), sliceLength );
        class SlicedStream : IReadStreamKnownPositionAndLength
        {
            readonly IReadStreamKnownPosition _stream;
            public SlicedStream( IReadStreamKnownPosition stream, long sliceLength )
            {
                _stream = stream;
                Length = sliceLength;
            }

            public long Length { get; }

            public long Position => _stream.Position;

            public int Read( Span<byte> buffer )
            {
                int amount = buffer.Length;
                long finalPos = amount + Position;
                int amountToRead = finalPos > Length ?
                      (int)(Length - Position)
                    : amount;
                int amountRead = _stream.Read( buffer.Slice( 0, amountToRead ) );
                Debug.Assert( Position <= Length );
                return amountRead;
            }
        }
    }
    
}
