# API docs

## Export

### Export Transactions

#### Export Transactions Request~~

```http request
POST /api/v1/users/{userId}/transactions/export
```

```json
{
    "startDate": "2021-01-01",
    "endDate": "2021-12-31"
}
```

#### Export Transactions Response

```http request
200 OK
```

```json
{
  "transactions": [
  ],
  "categories": [
  ]
}
```