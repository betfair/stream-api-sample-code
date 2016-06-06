using Betfair.ESASwagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Protocol
{
    /// <summary>
    /// Adapts market or order changes to a common change message
    /// </summary>
    public class ChangeMessageFactory
    {

        public static ChangeMessage<MarketChange> ToChangeMessage(MarketChangeMessage message)
        {
            ChangeMessage<MarketChange> change = new ChangeMessage<MarketChange>()
            {
                Id = (int)message.Id,
                Pt = message.Pt,
                Clk = message.Clk,
                InitialClk = message.InitialClk,
                ConflateMs = message.ConflateMs,
                HeartbeatMs = message.HeartbeatMs,
            };

            change.Items = message.Mc;

            switch (message.SegmentType)
            {
                case MarketChangeMessage.SegmentTypeEnum.SegStart:
                    change.SegmentType = SegmentType.SEG_START;
                    break;
                case MarketChangeMessage.SegmentTypeEnum.SegEnd:
                    change.SegmentType = SegmentType.SEG_END;
                    break;
                case MarketChangeMessage.SegmentTypeEnum.Seg:
                    change.SegmentType = SegmentType.SEG;
                    break;

            }
            switch (message.Ct)
            {
                case MarketChangeMessage.CtEnum.Heartbeat:
                    change.ChangeType = ChangeType.HEARTBEAT;
                    break;
                case MarketChangeMessage.CtEnum.ResubDelta:
                    change.ChangeType = ChangeType.RESUB_DELTA;
                    break;
                case MarketChangeMessage.CtEnum.SubImage:
                    change.ChangeType = ChangeType.SUB_IMAGE;
                    break;
            }
            return change;
        }

        public static ChangeMessage<OrderMarketChange> ToChangeMessage(OrderChangeMessage message)
        {
            ChangeMessage<OrderMarketChange> change = new ChangeMessage<OrderMarketChange>()
            {
                Id = (int)message.Id,
                Pt = message.Pt,
                Clk = message.Clk,
                InitialClk = message.InitialClk,
                ConflateMs = message.ConflateMs,
                HeartbeatMs = message.HeartbeatMs,
            };

            change.Items = message.Oc;

            switch (message.SegmentType)
            {
                case OrderChangeMessage.SegmentTypeEnum.SegStart:
                    change.SegmentType = SegmentType.SEG_START;
                    break;
                case OrderChangeMessage.SegmentTypeEnum.SegEnd:
                    change.SegmentType = SegmentType.SEG_END;
                    break;
                case OrderChangeMessage.SegmentTypeEnum.Seg:
                    change.SegmentType = SegmentType.SEG;
                    break;

            }
            switch (message.Ct)
            {
                case OrderChangeMessage.CtEnum.Heartbeat:
                    change.ChangeType = ChangeType.HEARTBEAT;
                    break;
                case OrderChangeMessage.CtEnum.ResubDelta:
                    change.ChangeType = ChangeType.RESUB_DELTA;
                    break;
                case OrderChangeMessage.CtEnum.SubImage:
                    change.ChangeType = ChangeType.SUB_IMAGE;
                    break;
            }
            return change;
        }
    }
}
