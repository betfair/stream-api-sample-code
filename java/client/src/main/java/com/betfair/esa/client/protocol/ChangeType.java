package com.betfair.esa.client.protocol;

/**
 * Common change type (as change type is local to market / order in swagger). Created by mulveyj on
 * 07/07/2016.
 */
public enum ChangeType {
    /** Update */
    UPDATE,
    /** Initial subscription image */
    SUB_IMAGE,
    /** Resubscription delta image */
    RESUB_DELTA,
    /** Heartbeat */
    HEARTBEAT,
}
