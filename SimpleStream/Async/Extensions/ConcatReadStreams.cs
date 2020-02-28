using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStream.Sync.AsyncExtensions
{
    public static class ConcatStreamAsyncExtensions
    {
        public static IAsyncReadStream ConcatReadStreams( this IEnumerable<IAsyncReadStream> streams )
            => new EnumerableStream( streams );
        class EnumerableStream : IAsyncReadStream
        {
            readonly IEnumerator<IAsyncReadStream> _streams;
            bool _init = false;
            bool _completed = false;
            public EnumerableStream( IEnumerable<IAsyncReadStream> streams )
            {
                _streams = streams.GetEnumerator();
            }

            public async ValueTask<int> ReadAsync( Memory<byte> buffer )
            {
                if( _completed == true ) return 0;
                if( !_init )
                {
                    _init = true;
                    if( !_streams.MoveNext() )
                    {
                        _completed = true;
                        return 0;
                    }
                }
                int readAmount = await _streams.Current.ReadAsync( buffer );
                while( readAmount == 0 )
                {
                    if( !_streams.MoveNext() )
                    {
                        _completed = true;
                        return 0;
                    }
                    readAmount = await _streams.Current.ReadAsync( buffer );
                }
                return readAmount;
            }
        }
    }
}
