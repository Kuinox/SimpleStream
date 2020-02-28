using SimpleStream.Sync.AsyncExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleStream.AsyncExtensions
{
    public static class AppendToAsyncExtensions
    {
        public static IAsyncReadStream AppendTo( this IAsyncReadStream @this, IAsyncReadStream appending )
            => new IAsyncReadStream[] { @this, appending}.ConcatReadStreams();

        public static IAsyncReadStream AppendTo( this IAsyncReadStream @this, Func<IAsyncReadStream> streamFactory )
            => new JustInTimeAppendToStream( @this, streamFactory );

        class JustInTimeAppendToStream : IAsyncReadStream
        {
            readonly IAsyncReadStream _streamA;
            readonly Func<IAsyncReadStream> _streamBFactory;
            IAsyncReadStream? _streamB;
            bool _streamACompleted = false;

            public JustInTimeAppendToStream( IAsyncReadStream streamA, Func<IAsyncReadStream> streamBFactory )
            {
                _streamA = streamA;
                _streamBFactory = streamBFactory;
            }

            public async ValueTask<int> ReadAsync( Memory<byte> buffer )
            {
                if( !_streamACompleted )
                {
                    int readCount = await _streamA.ReadAsync( buffer );
                    if( readCount > 0 ) return readCount;
                    _streamACompleted = true;
                }
                if(_streamB == null)
                {
                    _streamB = _streamBFactory();
                }
                return await _streamB.ReadAsync( buffer );
            }
        }

    }
}
