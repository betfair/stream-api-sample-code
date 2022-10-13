package com.betfair.esa.client.protocol;

import com.betfair.esa.swagger.model.MarketChange;
import com.betfair.esa.swagger.model.MarketChangeMessage;
import com.betfair.esa.swagger.model.OrderChangeMessage;
import com.betfair.esa.swagger.model.OrderMarketChange;

/** Adapts market or order changes to a common change message Created by mulveyj on 07/07/2016. */
public class ChangeMessageFactory {

    public static ChangeMessage<MarketChange> ToChangeMessage(MarketChangeMessage message) {
        ChangeMessage<MarketChange> change = new ChangeMessage<MarketChange>();
        change.setId(message.getId());
        change.setPublishTime(message.getPt());
        change.setClk(message.getClk());
        change.setInitialClk(message.getInitialClk());
        change.setConflateMs(message.getConflateMs());
        change.setHeartbeatMs(message.getHeartbeatMs());

        change.setItems(message.getMc());

        SegmentType segmentType = SegmentType.NONE;
        if (message.getSegmentType() != null) {
            switch (message.getSegmentType()) {
                case SEG_START -> segmentType = SegmentType.SEG_START;
                case SEG_END -> segmentType = SegmentType.SEG_END;
                case SEG -> segmentType = SegmentType.SEG;
            }
        }
        change.setSegmentType(segmentType);

        ChangeType changeType = ChangeType.UPDATE;
        if (message.getCt() != null) {
            switch (message.getCt()) {
                case HEARTBEAT -> changeType = ChangeType.HEARTBEAT;
                case RESUB_DELTA -> changeType = ChangeType.RESUB_DELTA;
                case SUB_IMAGE -> changeType = ChangeType.SUB_IMAGE;
            }
        }
        change.setChangeType(changeType);

        return change;
    }

    public static ChangeMessage<OrderMarketChange> ToChangeMessage(OrderChangeMessage message) {
        ChangeMessage<OrderMarketChange> change = new ChangeMessage<>();
        change.setId(message.getId());
        change.setPublishTime(message.getPt());
        change.setClk(message.getClk());
        change.setInitialClk(message.getInitialClk());
        change.setConflateMs(message.getConflateMs());
        change.setHeartbeatMs(message.getHeartbeatMs());

        change.setItems(message.getOc());

        SegmentType segmentType = SegmentType.NONE;
        if (message.getSegmentType() != null) {
            switch (message.getSegmentType()) {
                case SEG_START -> segmentType = SegmentType.SEG_START;
                case SEG_END -> segmentType = SegmentType.SEG_END;
                case SEG -> segmentType = SegmentType.SEG;
            }
        }
        change.setSegmentType(segmentType);

        ChangeType changeType = ChangeType.UPDATE;
        if (message.getCt() != null) {
            switch (message.getCt()) {
                case HEARTBEAT -> changeType = ChangeType.HEARTBEAT;
                case RESUB_DELTA -> changeType = ChangeType.RESUB_DELTA;
                case SUB_IMAGE -> changeType = ChangeType.SUB_IMAGE;
            }
        }
        change.setChangeType(changeType);

        return change;
    }
}
