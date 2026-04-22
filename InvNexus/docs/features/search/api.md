# SearchService API

## Base route
- `/api/search/products`

## Authentication requirement
All endpoints require JWT Bearer token.
- Token is issued by `AuthService`
- Client sends `Authorization: Bearer <token>`
- SearchService validates token

## Endpoint list
- `GET /api/search/products`

## Request/response examples
### `GET /api/search/products`
Response
```json
[
  {
    "id": "guid",
    "productId": "guid",
    "name": "Product A",
    "price": 100,
    "quantity": 12
  }
]
```

## Status code notes
- `200 OK`: successful read
- `401 Unauthorized`: missing/invalid token
