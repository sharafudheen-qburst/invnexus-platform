# InventoryService Specification

## Purpose
Manage product master data and current stock levels for phase 1.

## Scope
- Create and list products
- Keep one stock record per product
- Provide current stock by product
- Apply stock increases from purchase receipts
- Apply stock decreases from completed sales

## Main entities
### Product
- `Id` (Guid)
- `Name` (string, required)
- `Price` (decimal, `>= 0`)
- `IsActive` (bool)

### Stock
- `Id` (Guid)
- `ProductId` (Guid)
- `Quantity` (int)

## CQRS summary
### Commands
- `CreateProductCommand`
- `UpdateStockCommand`

### Queries
- `GetProductsQuery`
- `GetStockByProductIdQuery`

## Out of scope
- Warehouse transfer
- Stock adjustment history
- Advanced reporting
- Any pricing/tax complexity beyond stored unit price
