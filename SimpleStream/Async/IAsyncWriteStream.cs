using System;
using System.Threading.Tasks;

namespace SimpleStream
{
    public interface IAsyncWriteStream
    {
        ValueTask WriteAsync( ReadOnlyMemory<byte> buffer );
    }

    public interface IAsyncWriteStreamKnownLength : IHaveLength
    {
    }

    public interface IAsyncWriteStreamKnownPosition : IHavePosition
    {
    }

    public interface IAsyncWriteStreamKnownPositionAndLength : IAsyncWriteStreamKnownLength, IAsyncWriteStreamKnownPosition
    {
    }
}
