using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStream
{
    public interface IAsyncReadStream
    {
        ValueTask<int> ReadAsync( Memory<byte> buffer );
    }
    public interface IAsyncReadStreamKnownLength : IAsyncReadStream, IHaveLength
    {
    }

    public interface IAsyncReadStreamKnownPosition : IAsyncReadStream, IHavePosition
    {
    }
    public interface IAsyncReadStreamKnownPositionAndLength : IAsyncReadStreamKnownLength, IAsyncReadStreamKnownPosition
    {
    }
}
