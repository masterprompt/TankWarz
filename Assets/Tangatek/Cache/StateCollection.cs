using System;
using System.Collections.Generic;

namespace Tangatek.Caching
{
    public interface StateCollection
    {
        void Sleep();
        void Waken();
    }
}
