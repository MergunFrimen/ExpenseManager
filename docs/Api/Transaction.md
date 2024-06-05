# API docs

## Transaction

### Create Transaction

#### Create Transaction Request

```http request
POST /users/{userId}/transactions
```

```json
{
  "type": "Expense",
  "category": "Food",
  "description": "Today's lunch",
  "price": 100.00,
  "date": "2021-01-01T00:00:00Z"
}
```

#### Create Transaction Response

```http
201 Created
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "type": "Expense",
  "category": "Food",
  "description": "Today's lunch",
  "price": 100.00,
  "date": "2021-01-01T00:00:00Z"
}
```

### Get Balance

#### Get Balance Request

```http request
GET /users/{userId}/balance
```

#### Get Balance Response

```http
200 OK
```

```json
{
    "balance": 1000.00
}
```
