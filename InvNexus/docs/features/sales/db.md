# SalesService Database

## Tables
- `SalesOrders`
- `SalesOrderItems`

## Important columns
### `SalesOrders`
- `Id` (PK, uniqueidentifier)
- `SalesNumber` (nvarchar, unique/business identifier)
- `Status` (nvarchar: `Draft`, `Created`, `Completed`)
- `CreatedAt` (datetime)

### `SalesOrderItems`
- `Id` (PK, uniqueidentifier)
- `SalesOrderId` (FK to `SalesOrders.Id`)
- `ProductId` (uniqueidentifier)
- `Quantity` (int, `> 0`)
- `UnitPrice` (decimal, `>= 0`)

## Relationships
- One `SalesOrder` has many `SalesOrderItems`
- `SalesOrderItems.SalesOrderId` references `SalesOrders.Id`

## Notes for first version
- EF Core Code First with SQL Server / LocalDB
- Stock is not stored in SalesService
- On sales completion, publish integration message (for InventoryService stock decrease)
