using System;

namespace SimpleStream
{
    public interface IWriteStream
    {
        void Write( Span<byte> buffer );
    }

    public interface IWriteStreamKnownLength : IHaveLength
    {
    }

    public interface IWriteStreamKnownPosition : IHavePosition
    {
    }

    public interface IWriteStreamKnownPositionAndLength : IWriteStreamKnownLength, IWriteStreamKnownPosition
    {
    }
}
