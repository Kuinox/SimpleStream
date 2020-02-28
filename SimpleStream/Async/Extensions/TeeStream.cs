using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStream.Sync.AsyncExtensions
{
    public static class TeeStreamAsyncExtensions
    {
        public static IAsyncWriteStream CreateTeeStream( this ICollection<IAsyncWriteStream> streams )
            => new TeeStream( streams );

        class TeeStream : IAsyncWriteStream
        {
            readonly ICollection<IAsyncWriteStream> _streams;

            public TeeStream( ICollection<IAsyncWriteStream> streams )
            {
                _streams = streams;
            }

            public async ValueTask WriteAsync( ReadOnlyMemory<byte> buffer )
            {
                foreach( var stream in _streams )
                {
                    await stream.WriteAsync( buffer );
                } 
            }
        }
    }
}
