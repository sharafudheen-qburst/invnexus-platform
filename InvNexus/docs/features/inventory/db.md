# InventoryService Database

## Tables
- `Products`
- `Stocks`

## Important columns
### `Products`
- `Id` (PK, uniqueidentifier)
- `Name` (nvarchar, required)
- `Price` (decimal, non-negative)
- `IsActive` (bit)

### `Stocks`
- `Id` (PK, uniqueidentifier)
- `ProductId` (FK to `Products.Id`, unique for one-to-one)
- `Quantity` (int, non-negative)

## Relationships
- One `Product` has one `Stock`
- `Stocks.ProductId` references `Products.Id`

## Notes for first version
- EF Core Code First with SQL Server / LocalDB
- Create stock row with `Quantity = 0` when product is created
- Keep `Quantity` as current available stock only
- No stock movement history table in phase 1
