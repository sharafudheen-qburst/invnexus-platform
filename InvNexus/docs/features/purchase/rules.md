# PurchaseService Rules

## Business rules
- Purchase order must include at least one item
- Stock must not increase when order is only created
- Stock increases only when goods receipt is completed

## Validation rules
- `Items` must not be empty
- For each item: `Quantity > 0`
- For each item: `UnitPrice >= 0`

## State transition rules
- Allowed statuses: `Draft`, `Created`, `Received`
- Phase 1 flow uses:
  - Create order -> `Created`
  - Receive goods -> `Received`
- Receiving is allowed only from `Created`

## Stock effect rules
- `Created`: no stock effect
- `Received`: publish `GoodsReceived` event/message
- InventoryService uses `GoodsReceived` to increase stock
