using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStream.Sync.Extensions
{
    public static class WriteWhatYouReadExtension
    {
        public static IReadStream WriteWhatYouRead( this IReadStream @this, IWriteStream output )
            => new WriteWhatYouReadStream( @this, output );
        class WriteWhatYouReadStream : IReadStream
        {
            readonly IReadStream _baseStream;
            readonly IWriteStream _writeStream;

            public WriteWhatYouReadStream( IReadStream baseStream, IWriteStream writeStream )
            {
                _baseStream = baseStream;
                _writeStream = writeStream;
            }

            public int Read( Span<byte> buffer )
            {
                int amount = _baseStream.Read( buffer );
                _writeStream.Write( buffer.Slice( 0, amount ) );
                return amount;
            }
        }
    }
}
