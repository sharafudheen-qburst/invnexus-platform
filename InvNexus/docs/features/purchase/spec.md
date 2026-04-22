# PurchaseService Specification

## Purpose
Manage purchase orders, complete goods receipt, and trigger stock increase in InventoryService.

## Scope
- Create purchase orders with items
- List purchase orders
- Get purchase order details
- Receive goods for a purchase order
- Publish integration message when goods are received

## Main entities
### PurchaseOrder
- `Id` (Guid)
- `PurchaseNumber` (string)
- `Status` (string: `Draft`, `Created`, `Received`)
- `CreatedAt` (datetime)

### PurchaseOrderItem
- `Id` (Guid)
- `PurchaseOrderId` (Guid)
- `ProductId` (Guid)
- `Quantity` (int)
- `UnitPrice` (decimal)

## CQRS summary
### Commands
- `CreatePurchaseOrderCommand`
- `ReceiveGoodsCommand`

### Queries
- `GetPurchaseOrdersQuery`
- `GetPurchaseOrderByIdQuery`

## Out of scope
- Supplier portal
- Invoice and payment processing
- Multi-stage receiving complexity
- Advanced analytics/reporting
