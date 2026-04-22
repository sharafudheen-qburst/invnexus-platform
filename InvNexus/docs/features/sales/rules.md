# SalesService Rules

## Business rules
- Sales order must include at least one item
- Stock must not decrease when order is only created
- Stock decreases only when sale is completed

## Validation rules
- `Items` must not be empty
- For each item: `Quantity > 0`
- For each item: `UnitPrice >= 0`

## State transition rules
- Allowed statuses: `Draft`, `Created`, `Completed`
- Phase 1 flow uses:
  - Create order -> `Created`
  - Complete sale -> `Completed`
- Completion is allowed only from `Created`

## Stock effect rules
- `Created`: no stock effect
- `Completed`: publish `SalesCompleted` event/message
- InventoryService applies stock decrease and must prevent negative stock
