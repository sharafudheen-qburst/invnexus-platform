# SalesService API

## Base route
- `/api/sales`

## Authentication requirement
All endpoints require JWT Bearer token.
- Token is issued by `AuthService`
- Client sends `Authorization: Bearer <token>`
- SalesService validates token

## Endpoint list
- `POST /api/sales`
- `GET /api/sales`
- `GET /api/sales/{id}`
- `POST /api/sales/{id}/complete`

## Request/response examples
### `POST /api/sales`
Request
```json
{
  "items": [
    {
      "productId": "guid",
      "quantity": 2,
      "unitPrice": 150
    }
  ]
}
```

Response
```json
{
  "id": "guid",
  "salesNumber": "SO-0001",
  "status": "Created"
}
```

### `GET /api/sales`
Response
```json
[
  {
    "id": "guid",
    "salesNumber": "SO-0001",
    "status": "Created",
    "createdAt": "2026-04-21T10:00:00Z"
  }
]
```

### `GET /api/sales/{id}`
Response
```json
{
  "id": "guid",
  "salesNumber": "SO-0001",
  "status": "Created",
  "createdAt": "2026-04-21T10:00:00Z",
  "items": [
    {
      "productId": "guid",
      "quantity": 2,
      "unitPrice": 150
    }
  ]
}
```

### `POST /api/sales/{id}/complete`
Request
```json
{}
```

Response
```json
{
  "id": "guid",
  "salesNumber": "SO-0001",
  "status": "Completed"
}
```

## Status code notes
- `200 OK`: successful read/action
- `201 Created`: sales order created
- `400 Bad Request`: validation or invalid status transition
- `401 Unauthorized`: missing/invalid token
- `404 Not Found`: sales order not found
- `409 Conflict`: complete attempted in invalid state
