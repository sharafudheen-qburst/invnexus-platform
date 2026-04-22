# PurchaseService API

## Base route
- `/api/purchases`

## Authentication requirement
All endpoints require JWT Bearer token.
- Token is issued by `AuthService`
- Client sends `Authorization: Bearer <token>`
- PurchaseService validates token

## Endpoint list
- `POST /api/purchases`
- `GET /api/purchases`
- `GET /api/purchases/{id}`
- `POST /api/purchases/{id}/receive`

## Request/response examples
### `POST /api/purchases`
Request
```json
{
  "items": [
    {
      "productId": "guid",
      "quantity": 10,
      "unitPrice": 100
    }
  ]
}
```

Response
```json
{
  "id": "guid",
  "purchaseNumber": "PO-0001",
  "status": "Created"
}
```

### `GET /api/purchases`
Response
```json
[
  {
    "id": "guid",
    "purchaseNumber": "PO-0001",
    "status": "Created",
    "createdAt": "2026-04-21T10:00:00Z"
  }
]
```

### `GET /api/purchases/{id}`
Response
```json
{
  "id": "guid",
  "purchaseNumber": "PO-0001",
  "status": "Created",
  "createdAt": "2026-04-21T10:00:00Z",
  "items": [
    {
      "productId": "guid",
      "quantity": 10,
      "unitPrice": 100
    }
  ]
}
```

### `POST /api/purchases/{id}/receive`
Request
```json
{}
```

Response
```json
{
  "id": "guid",
  "purchaseNumber": "PO-0001",
  "status": "Received"
}
```

## Status code notes
- `200 OK`: successful read/action
- `201 Created`: purchase order created
- `400 Bad Request`: validation or invalid status transition
- `401 Unauthorized`: missing/invalid token
- `404 Not Found`: purchase order not found
- `409 Conflict`: receive attempted in invalid state
