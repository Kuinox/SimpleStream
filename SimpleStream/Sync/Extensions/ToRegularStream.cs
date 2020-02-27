using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleStream.Sync.Extensions
{
    public static class ToRegularStreamExtensions
    {
        public static Stream ToRegularStream( this IReadStream @this )
            => new ReadStreamWrapper( @this );
        class ReadStreamWrapper : Stream
        {
            readonly IReadStream _readStream;

            public ReadStreamWrapper( IReadStream readStream )
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
                => _readStream.Read( buffer.AsSpan( offset, count ) );

            public override int Read( Span<byte> buffer )
                => _readStream.Read( buffer );

            public override long Seek( long offset, SeekOrigin origin ) => throw new NotSupportedException();

            public override void SetLength( long value ) => throw new NotSupportedException();

            public override void Write( byte[] buffer, int offset, int count ) => throw new NotSupportedException();
        }
    }
}
