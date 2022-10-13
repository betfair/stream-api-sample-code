package com.betfair.esa.client.protocol;

import com.betfair.esa.swagger.model.RequestMessage;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.slf4j.profiler.StopWatch;

/**
 * Generic subscription handler for change messages: 1) Tracks clocks to facilitate resubscripiton
 * 2) Provides useful timings for initial image 3) Supports the ability to re-combine segmented
 * messages to retain event level atomicity Created by mulveyj on 07/07/2016.
 */
public class SubscriptionHandler<S extends RequestMessage, C extends ChangeMessage<I>, I> {
    private static final Logger LOG = LoggerFactory.getLogger(SubscriptionHandler.class);
    private final int subscriptionId;
    private final S subscriptionMessage;
    private boolean isSubscribed;
    private final boolean isMergeSegments;
    private List<I> mergedChanges;
    private final StopWatch ttfm;
    private final StopWatch ttlm;
    private int itemCount;
    private final CountDownLatch subscriptionComplete = new CountDownLatch(1);

    private long lastPublishTime;
    private long lastArrivalTime;
    private String initialClk;
    private String clk;
    private Long heartbeatMs;
    private Long conflationMs;

    public SubscriptionHandler(S subscriptionMessage, boolean isMergeSegments) {
        this.subscriptionMessage = subscriptionMessage;
        this.isMergeSegments = isMergeSegments;
        isSubscribed = false;
        subscriptionId = subscriptionMessage.getId();
        ttfm = new StopWatch("ttfm");
        ttlm = new StopWatch("ttlm");
    }

    public S getSubscriptionMessage() {
        return subscriptionMessage;
    }

    public boolean isSubscribed() {
        return isSubscribed;
    }

    public long getLastPublishTime() {
        return lastPublishTime;
    }

    public long getLastArrivalTime() {
        return lastArrivalTime;
    }

    public Long getHeartbeatMs() {
        return heartbeatMs;
    }

    public Long getConflationMs() {
        return conflationMs;
    }

    public String getInitialClk() {
        return initialClk;
    }

    public String getClk() {
        return clk;
    }

    void cancel() {
        // unwind waiters
        subscriptionComplete.countDown();
    }

    public C processChangeMessage(C changeMessage) {
        if (subscriptionId != changeMessage.getId()) {
            // previous subscription id - ignore
            return null;
        }

        // Every message store timings
        lastPublishTime = changeMessage.getPublishTime();
        lastArrivalTime = changeMessage.getArrivalTime();

        if (changeMessage.isStartOfRecovery()) {
            // Start of recovery
            ttfm.stop();
            LOG.info("{}: Start of image", subscriptionMessage.getOp());
        }

        if (changeMessage.getChangeType() == ChangeType.HEARTBEAT) {
            // Swallow heartbeats
            changeMessage = null;
        } else if (changeMessage.getSegmentType() != SegmentType.NONE && isMergeSegments) {
            // Segmented message and we're instructed to merge (which makes segments look atomic).
            changeMessage = MergeMessage(changeMessage);
        }

        if (changeMessage != null) {
            // store clocks
            if (changeMessage.getInitialClk() != null) {
                initialClk = changeMessage.getInitialClk();
            }
            if (changeMessage.getClk() != null) {
                clk = changeMessage.getClk();
            }

            if (!isSubscribed) {
                // During recovery
                if (changeMessage.getItems() != null) {
                    itemCount += changeMessage.getItems().size();
                }
            }

            if (changeMessage.isEndOfRecovery()) {
                // End of recovery
                isSubscribed = true;
                heartbeatMs = changeMessage.getHeartbeatMs();
                conflationMs = changeMessage.getConflateMs();
                ttlm.stop();
                LOG.info(
                        "{}: End of image: type:{}, ttfm:{}, ttlm:{}, conflation:{}, heartbeat:{},"
                                + " change.items:{}",
                        subscriptionMessage.getOp(),
                        changeMessage.getChangeType(),
                        ttfm,
                        ttlm,
                        conflationMs,
                        heartbeatMs,
                        itemCount);

                // unwind future
                subscriptionComplete.countDown();
            }
        }
        return changeMessage;
    }

    private C MergeMessage(C changeMessage) {
        // merge segmented messages so client sees atomic view across segments
        if (changeMessage.getSegmentType() == SegmentType.SEG_START) {
            // start merging
            mergedChanges = new ArrayList<>();
        }
        // accumulate
        mergedChanges.addAll(changeMessage.getItems());

        if (changeMessage.getSegmentType() == SegmentType.SEG_END) {
            // finish merging
            changeMessage.setSegmentType(SegmentType.NONE);
            changeMessage.setItems(mergedChanges);
            mergedChanges = null;
        } else {
            // swallow message as we're still merging
            changeMessage = null;
        }
        return changeMessage;
    }
}
