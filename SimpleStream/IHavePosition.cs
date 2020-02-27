using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStream
{
    public interface IHavePosition
    {
        public long Position { get; }
    }
}
