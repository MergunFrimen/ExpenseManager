# API docs

## Transaction

### Create Transaction

#### Create Transaction Request

```http request
POST /transactions
```

```json
{
  "type": "Expense",
  "categoryId": "00000000-0000-0000-0000-000000000000",
  "description": "Today's lunch",
  "amount": 100.00,
  "date": "2021-01-01T00:00:00Z"
}
```

#### Create Transaction Response

```http request
201 Created
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "type": "Expense",
  "categoryId": "00000000-0000-0000-0000-000000000000",
  "category": "Food",
  "description": "Today's lunch",
  "price": 100.00,
  "date": "2021-01-01T00:00:00Z"
}
```

### Update Transaction

#### Update Transaction Request

```http request
PUT /transactions
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "type": "Expense",
  "categoryId": "00000000-0000-0000-0000-000000000000",
  "description": "Today's lunch",
  "amount": 100.00,
  "date": "2021-01-01T00:00:00Z"
}
```

#### Update Transaction Response

```http request
200 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "type": "Expense",
  "categoryId": "00000000-0000-0000-0000-000000000000",
  "category": "Food",
  "description": "Today's lunch",
  "price": 100.00,
  "date": "2021-01-01T00:00:00Z"
}
```

### Delete Transaction

#### Delete Transaction Request

```http request
DELETE /transactions
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000"
}
```

#### Delete Transaction Response

```http request
200 OK
```

```json
{
}
```

### List Transactions

#### List Transactions Request

```http request
GET /transactions
```

```json
{
}
```

#### List Transactions Response

```http request
200 OK
```

```json
{
  "transactions": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "type": "Expense",
      "categoryId": "00000000-0000-0000-0000-000000000000",
      "category": "Food",
      "description": "Today's lunch",
      "price": 100.00,
      "date": "2021-01-01T00:00:00Z"
    },
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "type": "Expense",
      "categoryId": "00000000-0000-0000-0000-000000000000",
      "category": "Food",
      "description": "Today's lunch",
      "price": 100.00,
      "date": "2021-01-01T00:00:00Z"
    }
  ]
}
```
