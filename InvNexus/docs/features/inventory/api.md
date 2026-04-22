# InventoryService API

## Base route
- `/api/products`
- `/api/stock`

## Authentication requirement
All endpoints require JWT Bearer token.
- Token is issued by `AuthService`
- Client sends `Authorization: Bearer <token>`
- InventoryService validates token

## Endpoint list
- `POST /api/products`
- `GET /api/products`
- `GET /api/stock/{productId}`

## Request/response examples
### `POST /api/products`
Request
```json
{
  "name": "string",
  "price": 0,
  "isActive": true
}
```

Response
```json
{
  "id": "guid",
  "name": "string",
  "price": 0,
  "isActive": true
}
```

### `GET /api/products`
Response
```json
[
  {
    "id": "guid",
    "name": "string",
    "price": 0,
    "isActive": true
  }
]
```

### `GET /api/stock/{productId}`
Response
```json
{
  "productId": "guid",
  "quantity": 0
}
```

## Status code notes
- `200 OK`: successful read
- `201 Created`: product created
- `400 Bad Request`: validation error
- `401 Unauthorized`: missing/invalid token
- `404 Not Found`: product/stock not found
