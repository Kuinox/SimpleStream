using SimpleStream.Sync.Extensions;
using System;
using System.Collections.Generic;

namespace SimpleStream.Extensions
{
    public static class AppendToExtensions
    {
        public static IReadStream AppendTo( this IReadStream @this, IReadStream appending )
            => new IReadStream[] { @this, appending}.ConcatReadStreams();

        public static IReadStream AppendTo( this IReadStream @this, Func<IReadStream> streamFactory )
            => new JustInTimeAppendToStream( @this, streamFactory );

        class JustInTimeAppendToStream : IReadStream
        {
            readonly IReadStream _streamA;
            readonly Func<IReadStream> _streamBFactory;
            IReadStream? _streamB;
            bool _streamACompleted = false;

            public JustInTimeAppendToStream( IReadStream streamA, Func<IReadStream> streamBFactory )
            {
                _streamA = streamA;
                _streamBFactory = streamBFactory;
            }

            public int Read( Span<byte> buffer )
            {
                if( !_streamACompleted )
                {
                    int readCount = _streamA.Read( buffer );
                    if( readCount > 0 ) return readCount;
                    _streamACompleted = true;
                }
                if(_streamB == null)
                {
                    _streamB = _streamBFactory();
                }
                return _streamB.Read( buffer );
            }
        }

    }
}
