# Domain model docs

## Ledger Aggregate

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "userId": "00000000-0000-0000-0000-000000000000",
  "name": "Default",
  "transactions": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "type": "Expense",
      "category": "Food",
      "description": "Today's lunch",
      "price": 100.00,
      "date": "2021-01-01T00:00:00Z"
    },
    {
      "id": "00000000-0000-0000-0000-000000000001",
      "type": "Income",
      "category": "Salary",
      "description": "January salary",
      "price": 1000.00,
      "date": "2021-01-01T00:00:00Z"
    }
  ]
}
```

## Transaction Entity

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
