# API docs

## User Statistics

### User Statistics

#### User Statistics Request

```http request
POST /api/v1/users/{userId}/statistics
```

```json
{
  "from": "2021-01-01",
  "to": "2021-12-31",
  "groupBy": "day"
}
```

#### User Statistics Response
- either return data to create graphs with
- or return an image or something like that

```http request
200 OK
```

```json
{
  "totalBalance": 123,
  "totalExpense": 123,
  "totalIncome": 123
}
```