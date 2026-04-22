# SearchService Flow

## Step-by-step business flow
1. Product is created in InventoryService.
2. Product-created event is published.
3. SearchService receives the event and inserts `ProductSearchView` record.
4. PurchaseService completes goods receipt and publishes `GoodsReceived` event.
5. SearchService receives the event and increases `Quantity` in read model.
6. SalesService completes sale and publishes `SalesCompleted` event.
7. SearchService receives the event and decreases `Quantity` in read model.
8. UI calls `GET /api/search/products` for fast read results.

## Where stock changes
- No stock transaction is executed by SearchService
- Quantity in read model is updated to mirror stock-related events

## Integration event/message points
- Consumes product-created event from InventoryService
- Consumes `GoodsReceived` from PurchaseService
- Consumes `SalesCompleted` from SalesService
- Produces no outbound integration event in phase 1
