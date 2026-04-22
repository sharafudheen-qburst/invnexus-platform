# SearchService Database

## Database type
- MongoDB

## Collections
- `ProductSearchViews`

## Important fields
### `ProductSearchViews`
- `Id` (Guid)
- `ProductId` (Guid)
- `Name` (string)
- `Price` (decimal)
- `Quantity` (int)

## Relationships
- Denormalized single-document read model
- No relational joins in phase 1

## Notes for first version
- Data is updated by events from core services
- Keep documents optimized for UI reads
- Service is read-only for API consumers
