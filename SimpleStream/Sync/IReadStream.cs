using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStream
{
    public interface IReadStream
    {
        int Read( Span<byte> buffer );
    }

    public interface IReadStreamKnownLength : IReadStream, IHaveLength
    {
    }

    public interface IReadStreamKnownPosition : IReadStream, IHavePosition
    {
    }
    public interface IReadStreamKnownPositionAndLength : IReadStreamKnownLength, IReadStreamKnownPosition
    {
    }
}
