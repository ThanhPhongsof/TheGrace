using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGrace.Domain.Abstractions;
public static class SequentialGuid
{
    public static Guid NewGuid()
    {
        var guidBytes = Guid.NewGuid().ToByteArray();
        var timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

        // Swap timestamp into the GUID
        Array.Copy(timestamp, 0, guidBytes, guidBytes.Length - timestamp.Length, timestamp.Length);
        return new Guid(guidBytes);
    }
}
