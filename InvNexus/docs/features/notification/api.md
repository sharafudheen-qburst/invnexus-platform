# NotificationService API

## Base route
- `/api/notifications`

## Authentication requirement
All endpoints require JWT Bearer token.
- Token is issued by `AuthService`
- Client sends `Authorization: Bearer <token>`
- NotificationService validates token

## Endpoint list
- `GET /api/notifications`
- `GET /api/notifications/{id}`

## Request/response examples
### `GET /api/notifications`
Response
```json
[
  {
    "id": "guid",
    "type": "GoodsReceived",
    "message": "Goods received for purchase PO-0001",
    "createdAt": "2026-04-21T10:00:00Z"
  }
]
```

### `GET /api/notifications/{id}`
Response
```json
{
  "id": "guid",
  "type": "SalesCompleted",
  "message": "Sales order SO-0001 completed",
  "createdAt": "2026-04-21T10:05:00Z"
}
```

## Status code notes
- `200 OK`: successful read
- `401 Unauthorized`: missing/invalid token
- `404 Not Found`: notification id not found
