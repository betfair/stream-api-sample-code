using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Protocol
{
    public enum ChangeType
    {
        UPDATE,
        SUB_IMAGE,
        RESUB_DELTA,
        HEARTBEAT,
    }
}
