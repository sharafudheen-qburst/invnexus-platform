# NotificationService Rules

## Business rules
- Create a notification record when a supported event is received
- Supported events in phase 1:
  - `GoodsReceived`
  - `SalesCompleted`
- NotificationService is for logging only

## Validation rules
- `Type` is required
- `Message` is required
- `CreatedAt` is required

## State transition rules
- No workflow states in phase 1
- Records are created and then read

## Stock effect rules
- NotificationService does not change stock
- It only records event outcomes as log messages
