using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStream.Sync.Extensions
{
    public static class ConcatStreamExtensions
    {
        public static IReadStream ConcatReadStreams( this IEnumerable<IReadStream> streams )
            => new EnumerableStream( streams );
        class EnumerableStream : IReadStream
        {
            readonly IEnumerator<IReadStream> _streams;
            bool _init = false;
            bool _completed = false;
            public EnumerableStream( IEnumerable<IReadStream> streams )
            {
                _streams = streams.GetEnumerator();
            }

            public int Read( Span<byte> buffer )
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
                int readAmount = _streams.Current.Read( buffer );
                while(readAmount == 0)
                {
                    if( !_streams.MoveNext() )
                    {
                        _completed = true;
                        return 0;
                    }
                    readAmount = _streams.Current.Read( buffer );
                }
                return readAmount;
            }
        }
    }
}
