# PurchaseService Flow

## Step-by-step business flow
1. User authenticates via `AuthService` and gets JWT token.
2. User creates purchase order with one or more items.
3. PurchaseService validates data and stores order with status `Created`.
4. No stock change happens at creation time.
5. User triggers goods receipt for the purchase order.
6. PurchaseService validates current status and marks order as `Received`.
7. PurchaseService publishes `GoodsReceived` integration event/message.
8. InventoryService consumes the message and increases stock.

## Where stock changes
- No stock change on purchase order creation
- Stock increases only after goods receipt completion

## Integration event/message points
- Published by PurchaseService at goods receipt completion (`GoodsReceived`)
- Consumed by InventoryService for stock increase
