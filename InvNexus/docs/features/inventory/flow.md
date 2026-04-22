# InventoryService Flow

## Step-by-step business flow
1. User authenticates via `AuthService` and gets JWT token.
2. User creates product in InventoryService.
3. InventoryService stores product and creates stock row with quantity `0`.
4. PurchaseService completes goods receipt.
5. PurchaseService publishes `GoodsReceived` event/message.
6. InventoryService consumes the message and increases stock.
7. SalesService completes sales order.
8. SalesService publishes `SalesCompleted` event/message.
9. InventoryService consumes the message and decreases stock.
10. Users query current stock via `GET /api/stock/{productId}`.

## Where stock changes
- Increase on `GoodsReceived`
- Decrease on `SalesCompleted`

## Integration event/message points
- Published by PurchaseService when goods receipt is completed
- Published by SalesService when sale is completed
- Consumed by InventoryService to update current stock
