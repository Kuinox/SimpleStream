namespace SimpleStream
{
    public interface IAsyncReadWriteStream : IAsyncReadStream , IAsyncWriteStream
    {
    }

    public interface IAsyncReadWriteStreamKnownLength :
        IAsyncReadWriteStream,//base
        IAsyncReadStreamKnownLength, IAsyncWriteStreamKnownLength//RW equivalent
    {
    }
    public interface IAsyncReadWriteStreamKnownPosition :
        IAsyncReadWriteStream,//base
        IAsyncReadStreamKnownPosition, IAsyncWriteStreamKnownPosition//RW equivalent
    {
    }

    public interface IAsyncReadWriteStreamWithKnownPositionAndLength :
        IAsyncReadWriteStreamKnownLength, IAsyncReadWriteStreamKnownPosition, //base
        IAsyncReadStreamKnownPositionAndLength, IAsyncWriteStreamKnownPositionAndLength //RW equivalent
    {
    }
}
