using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStream.Sync.AsyncExtensions
{
    public static class WriteWhatYouReadExtension
    {
        public static IAsyncReadStream WriteWhatYouRead( this IAsyncReadStream @this, IAsyncWriteStream output )
            => new WriteWhatYouReadStream( @this, output );
        class WriteWhatYouReadStream : IAsyncReadStream
        {
            readonly IAsyncReadStream _baseStream;
            readonly IAsyncWriteStream _writeStream;

            public WriteWhatYouReadStream( IAsyncReadStream baseStream, IAsyncWriteStream writeStream )
            {
                _baseStream = baseStream;
                _writeStream = writeStream;
            }

            public async ValueTask<int> ReadAsync( Memory<byte> buffer )
            {
                int amount = await _baseStream.ReadAsync( buffer );
                await _writeStream.WriteAsync( buffer.Slice( 0, amount ) );
                return amount;
            }
        }
    }
}
