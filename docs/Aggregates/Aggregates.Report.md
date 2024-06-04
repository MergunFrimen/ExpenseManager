# Domain Models

## Report

```csharp
public class Report
{
    void Update(Report report);
}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "user_id": "00000000-0000-0000-0000-000000000000",
    "transaction_id": "00000000-0000-0000-0000-000000000000",
    "amount": 100,
    "type": "credit",
    "status": "completed",
    "created_at": "2021-01-01T00:00:00Z",
    "updated_at": "2021-01-01T00:00:00Z"
}
```