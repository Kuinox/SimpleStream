namespace SimpleStream
{
    public interface IReadWriteStream : IReadStream , IWriteStream
    {
    }

    public interface IReadWriteStreamKnownLength :
        IReadWriteStream,//base
        IReadStreamKnownLength, IWriteStreamKnownLength//RW equivalent
    {
    }
    public interface IReadWriteStreamKnownPosition :
        IReadWriteStream,//base
        IReadStreamKnownPosition, IWriteStreamKnownPosition//RW equivalent
    {
    }

    public interface IReadWriteStreamWithKnownPositionAndLength :
        IReadWriteStreamKnownLength, IReadWriteStreamKnownPosition, //base
        IReadStreamKnownPositionAndLength, IWriteStreamKnownPositionAndLength //RW equivalent
    {
    }
}
