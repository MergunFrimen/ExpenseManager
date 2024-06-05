# Domain model docs

## Ledger

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "userId": "00000000-0000-0000-0000-000000000000",
    "name": "Personal",
    "description": "Personal expenses",
    "balance": 1000.00,
    "transactions": [
        {
            "id": "00000000-0000-0000-0000-000000000000",
            "ledgerId": "00000000-0000-0000-0000-000000000000",
            "type": "Expense",
            "category": "Food",
            "description": "Today's lunch",
            "price": 100.00,
            "date": "2021-01-01T00:00:00Z"
        },
        {
            "id": "00000000-0000-0000-0000-000000000000",
            "ledgerId": "00000000-0000-0000-0000-000000000000",
            "type": "Income",
            "category": "Salary",
            "description": "Monthly salary",
            "price": 100.00,
            "date": "2021-01-01T00:00:00Z"
        }
    ],
    "categories": [
        "Food",
        "Salary"
    ]
}
```

## Transaction

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
