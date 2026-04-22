# InventoryService Rules

## Business rules
- Product creation is master-data only
- One product must have one stock record
- New product starts with stock quantity `0`

## Validation rules
- `Name` is required
- `Price >= 0`
- Stock quantity cannot be negative

## State transition rules
- InventoryService has no order status workflow
- Stock changes only through business events/messages from other services

## Stock effect rules
- Purchase goods receipt increases stock
- Sales completion decreases stock
- Update must be rejected if decrease would make stock below zero
