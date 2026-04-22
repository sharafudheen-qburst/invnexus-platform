# NotificationService Flow

## Step-by-step business flow
1. PurchaseService completes goods receipt and publishes `GoodsReceived` event.
2. NotificationService receives the event.
3. NotificationService creates a `Notification` document with type `GoodsReceived`.
4. SalesService completes sales order and publishes `SalesCompleted` event.
5. NotificationService receives the event.
6. NotificationService creates a `Notification` document with type `SalesCompleted`.
7. UI reads notification logs using notification APIs.

## Where stock changes
- No stock changes in NotificationService

## Integration event/message points
- Consumes `GoodsReceived` from PurchaseService
- Consumes `SalesCompleted` from SalesService
- Produces no outbound integration event in phase 1
