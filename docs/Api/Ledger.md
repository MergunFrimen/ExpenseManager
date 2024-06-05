# API docs

## Ledger

### Create transaction

#### Create transaction Request

```http request
POST /api/ledger/{userId}/transactions
```

```json
{
    "ledgerId": "00000000-0000-0000-0000-000000000000",
    "type": "Expense",
    "category": "Food",
    "description": "Today's lunch",
    "price": 100.00,
    "date": "2021-01-01T00:00:00Z"
}
```

#### Create transaction Response

```http
201 Created
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "ledgerId": "00000000-0000-0000-0000-000000000000",
    "type": "Expense",
    "category": "Food",
    "description": "Today's lunch",
    "price": 100.00,
    "date": "2021-01-01T00:00:00Z"
}
```

### Get balance

#### Get balance Request

```http request
GET /api/ledger/{userId}/balance
```

#### Get balance Response

```http
200 OK
```

```json
{
    "balance": 1000.00
}
```
