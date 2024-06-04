# Domain Models

## User

```csharp
public class User : AggregateRoot
{
    void 
    void Update(User user);
}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "John Doe",
    "email": "johndoe@gmail.com",
    "created_at": "2021-01-01T00:00:00Z",
    "updated_at": "2021-01-01T00:00:00Z"
}
```