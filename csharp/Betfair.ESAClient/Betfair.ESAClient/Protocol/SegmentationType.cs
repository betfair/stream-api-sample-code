using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Protocol
{
    /// <summary>
    /// Common segmentation type (as change type is local to market / order in swagger).
    /// </summary>
    public enum SegmentType
    {
        NONE,
        SEG_START,
        SEG,
        SEG_END,
    }
}
