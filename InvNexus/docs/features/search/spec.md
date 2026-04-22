# SearchService Specification

## Purpose
Provide a fast, read-only product and stock view for UI queries in phase 1.

## Scope
- Maintain denormalized read model from events
- Serve product + stock query endpoint
- Store read model in MongoDB

## Main entities
### ProductSearchView
- `Id` (Guid)
- `ProductId` (Guid)
- `Name` (string)
- `Price` (decimal)
- `Quantity` (int)

## CQRS summary
- Query-focused service in phase 1
- Read model updated asynchronously by integration events

## Out of scope
- Direct writes from UI
- Full text search complexity
- Advanced filtering/sorting/paging features
- Analytics/reporting features
