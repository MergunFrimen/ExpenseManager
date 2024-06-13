# API docs

## Import

### Import Transactions

#### Import Transactions Request

```http request
POST /api/v1/users/{userId}/transactions/import
```

```json
{
  "transactions": [
  ],
  "categories": [
  ]
}
```

#### Import Transactions Response

##### Success
- return list of imported transactions

```http request
201 Created
```

```json
{
}
```

##### Invalid data

```http request
400 Bad Request
```

```json
{
}
```