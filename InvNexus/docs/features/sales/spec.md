# SalesService Specification

## Purpose
Manage sales orders, complete sales, and trigger stock decrease in InventoryService.

## Scope
- Create sales orders with items
- List sales orders
- Get sales order details
- Complete sales orders
- Publish integration message when sale is completed

## Main entities
### SalesOrder
- `Id` (Guid)
- `SalesNumber` (string)
- `Status` (string: `Draft`, `Created`, `Completed`)
- `CreatedAt` (datetime)

### SalesOrderItem
- `Id` (Guid)
- `SalesOrderId` (Guid)
- `ProductId` (Guid)
- `Quantity` (int)
- `UnitPrice` (decimal)

## CQRS summary
### Commands
- `CreateSalesOrderCommand`
- `CompleteSalesOrderCommand`

### Queries
- `GetSalesOrdersQuery`
- `GetSalesOrderByIdQuery`

## Out of scope
- Customer portal
- Payment and invoicing workflows
- Promotions and discounts complexity
- Advanced reporting
