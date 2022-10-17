package com.betfair.esa.client.protocol;

import java.util.List;

/** Created by mulveyj on 07/07/2016. */
public class ChangeMessage<T> {
    private long arrivalTime;
    private long publishTime;
    private int id;
    private String clk;
    private String initialClk;
    private Long heartbeatMs;
    private Long conflateMs;
    private List<T> items;
    private SegmentType segmentType;
    private ChangeType changeType;

    public ChangeMessage() {
        arrivalTime = System.currentTimeMillis();
    }

    /**
     * Start of new subscription (not resubscription)
     *
     * @return
     */
    public boolean isStartOfNewSubscription() {
        return changeType == ChangeType.SUB_IMAGE
                && (segmentType == SegmentType.NONE || segmentType == SegmentType.SEG_START);
    }

    /**
     * Start of subscription / resubscription
     *
     * @return
     */
    public boolean isStartOfRecovery() {
        return (changeType == ChangeType.SUB_IMAGE || changeType == ChangeType.RESUB_DELTA)
                && (segmentType == SegmentType.NONE || segmentType == SegmentType.SEG_START);
    }

    /**
     * End of subscription / resubscription
     *
     * @return
     */
    public boolean isEndOfRecovery() {
        return (changeType == ChangeType.SUB_IMAGE || changeType == ChangeType.RESUB_DELTA)
                && (segmentType == SegmentType.NONE || segmentType == SegmentType.SEG_END);
    }

    public ChangeType getChangeType() {
        return changeType;
    }

    public void setChangeType(ChangeType changeType) {
        this.changeType = changeType;
    }

    public long getArrivalTime() {
        return arrivalTime;
    }

    public void setArrivalTime(long arrivalTime) {
        this.arrivalTime = arrivalTime;
    }

    public long getPublishTime() {
        return publishTime;
    }

    public void setPublishTime(long publishTime) {
        this.publishTime = publishTime;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getClk() {
        return clk;
    }

    public void setClk(String clk) {
        this.clk = clk;
    }

    public String getInitialClk() {
        return initialClk;
    }

    public void setInitialClk(String initialClk) {
        this.initialClk = initialClk;
    }

    public Long getHeartbeatMs() {
        return heartbeatMs;
    }

    public void setHeartbeatMs(Long heartbeatMs) {
        this.heartbeatMs = heartbeatMs;
    }

    public Long getConflateMs() {
        return conflateMs;
    }

    public void setConflateMs(Long conflateMs) {
        this.conflateMs = conflateMs;
    }

    public List<T> getItems() {
        return items;
    }

    public void setItems(List<T> items) {
        this.items = items;
    }

    public SegmentType getSegmentType() {
        return segmentType;
    }

    public void setSegmentType(SegmentType segmentType) {
        this.segmentType = segmentType;
    }
}
