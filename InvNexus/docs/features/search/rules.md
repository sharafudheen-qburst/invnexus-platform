# SearchService Rules

## Business rules
- Search data is maintained through integration events
- UI must not write directly to SearchService
- API is read-only in phase 1

## Validation rules
- `ProductId` is required
- `Name` is required
- `Price >= 0`
- `Quantity >= 0`

## State transition rules
- No order/status workflow in SearchService
- Records are inserted/updated based on incoming events

## Stock effect rules
- SearchService does not own stock transactions
- It reflects latest quantity from event-driven updates
