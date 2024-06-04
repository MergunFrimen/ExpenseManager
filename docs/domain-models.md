# Domain models

## User

```csharp
public class User : AggregateRoot
{
    // TODO: add more methods
}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "John",
    "lastName": "Doe",
    "email": "johndoe@gmail.com",
    "password": "password",
    "transactions": [
      ...
    ]
}
```

## Transaction

```csharp
public class Transaction
{
    // add more methods
}
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "user_id": "00000000-0000-0000-0000-000000000000",
  "type": "Expense",
  "category": "Food",
  "description": "Today's lunch",
  "price": {
    "amount": 100.00,
    "currency": "USD"
  },
  "date": "2021-01-01T00:00:00Z"
}
```