# API docs

## User Balance

### User Balance

#### User Balance Request

```http request
GET /api/v1/users/{userId}/balance
```

```json
{
}
```

#### User Balance Response

```http request
200 OK
```

```json
{
  "userId": "string",
  "totalBalance": 123
}
```