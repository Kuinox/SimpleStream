using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleStream.Sync.AsyncExtensions
{
    public static class ToRegularStreamAsyncExtensions
    {
        public static Stream ToRegularStream( this IAsyncReadStream @this )
            => new ReadStreamWrapper( @this );
        class ReadStreamWrapper : Stream
        {
            readonly IAsyncReadStream _readStream;

            public ReadStreamWrapper( IAsyncReadStream readStream )
            {
                _readStream = readStream;
            }

            public override bool CanRead => true;

            public override bool CanSeek => false;

            public override bool CanWrite => false;

            public override long Length => throw new NotSupportedException();

            public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

            public override void Flush() => throw new NotSupportedException();

            public override int Read( byte[] buffer, int offset, int count )
                => _readStream.ReadAsync( buffer.AsMemory( offset, count ) ).Result;

            public override Task<int> ReadAsync( byte[] buffer, int offset, int count, CancellationToken cancellationToken )
                => _readStream.ReadAsync( buffer.AsMemory( offset, count ) ).AsTask();

            public override ValueTask<int> ReadAsync( Memory<byte> buffer, CancellationToken cancellationToken = default )
                => _readStream.ReadAsync( buffer );

            public override long Seek( long offset, SeekOrigin origin ) => throw new NotSupportedException();

            public override void SetLength( long value ) => throw new NotSupportedException();

            public override void Write( byte[] buffer, int offset, int count ) => throw new NotSupportedException();


        }
    }
}
