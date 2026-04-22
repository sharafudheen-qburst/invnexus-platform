# PurchaseService Database

## Tables
- `PurchaseOrders`
- `PurchaseOrderItems`

## Important columns
### `PurchaseOrders`
- `Id` (PK, uniqueidentifier)
- `PurchaseNumber` (nvarchar, unique/business identifier)
- `Status` (nvarchar: `Draft`, `Created`, `Received`)
- `CreatedAt` (datetime)

### `PurchaseOrderItems`
- `Id` (PK, uniqueidentifier)
- `PurchaseOrderId` (FK to `PurchaseOrders.Id`)
- `ProductId` (uniqueidentifier)
- `Quantity` (int, `> 0`)
- `UnitPrice` (decimal, `>= 0`)

## Relationships
- One `PurchaseOrder` has many `PurchaseOrderItems`
- `PurchaseOrderItems.PurchaseOrderId` references `PurchaseOrders.Id`

## Notes for first version
- EF Core Code First with SQL Server / LocalDB
- Stock is not stored in PurchaseService
- On goods receipt, publish integration message (for InventoryService stock increase)
