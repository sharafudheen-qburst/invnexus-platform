# SalesService Flow

## Step-by-step business flow
1. User authenticates via `AuthService` and gets JWT token.
2. User creates sales order with one or more items.
3. SalesService validates data and stores order with status `Created`.
4. No stock change happens at creation time.
5. User completes the sales order.
6. SalesService validates current status and marks order as `Completed`.
7. SalesService publishes `SalesCompleted` integration event/message.
8. InventoryService consumes the message and decreases stock.
9. InventoryService rejects update if resulting stock would be below zero.

## Where stock changes
- No stock change on sales order creation
- Stock decreases only after sales completion

## Integration event/message points
- Published by SalesService at sales completion (`SalesCompleted`)
- Consumed by InventoryService for stock decrease
