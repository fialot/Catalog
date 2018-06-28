using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katalog
{
    [Flags]
    public enum FastFlags : ushort
    {
        FLAG1 = 0x00000001,
        FLAG2 = 0x00000002,
        FLAG3 = 0x00000004,
        FLAG4 = 0x00000008,
        FLAG5 = 0x00000010,
        FLAG6 = 0x00000020,
    }

    static class global
    {

    }
}
