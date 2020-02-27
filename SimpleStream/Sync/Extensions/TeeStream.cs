using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStream.Sync.Extensions
{
    public static class TeeStreamExtensions
    {
        public static IWriteStream CreateTeeStream( this ICollection<IWriteStream> streams )
            => new TeeStream( streams );

        class TeeStream : IWriteStream
        {
            readonly ICollection<IWriteStream> _streams;

            public TeeStream( ICollection<IWriteStream> streams )
            {
                _streams = streams;
            }

            public void Write( Span<byte> buffer )
            {
                foreach( var stream in _streams )
                {
                    stream.Write( buffer );
                }
            }
        }
    }
}
