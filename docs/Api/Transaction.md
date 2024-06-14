# API docs

## Transaction

### Query Transactions

#### Query Transactions Request
- pagination, filters, sorting

```http request
POST /api/v1/users/{userId}/transactions/search
```

```json
{
  "pagination": {
    "pageSize": 10,
    "pageNumber": 1
  },
  "filters": {
    "descriptionFilter": "work",
    "dateRangeFilter": {
      "startDate": "2023-12-31",
      "endDate": "2024-06-30"
    },
    "categoryFilter": [
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b",
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b",
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b"
    ],
    "priceRangeFilter": {
      "minPrice": 123,
      "maxPrice": 1230
    }
  },
  "sorting": {
    "orderBy": "date",
    "orderDirection": "ascending"
  }
}
```

#### Query Transactions Response

```http request
200 OK
```

```json
[
  {
    "id": "00000000-0000-0000-0000-000000000000",
    "date": "2023-12-31",
    "description": "work",
    "price": 123,
    "tags": [
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b",
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b",
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b"
    ]
  },
  {
    "id": "00000000-0000-0000-0000-000000000000",
    "date": "2023-12-31",
    "description": "work",
    "price": 123,
    "tags": [
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b",
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b",
      "0e8b3b7b-0b3b-4b3b-8b3b-0b3b3b0b3b3b"
    ]
  }
]
```

### Get Transaction

#### Get Transaction Request

```http request
GET /api/v1/users/{userId}/transactions/{transactionId}
```

```json
{
}
```

#### Get Transaction Response

##### Success

```http request
200 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "userId": "00000000-0000-0000-0000-000000000000",
  "date": "2023-12-31",
  "description": "work",
  "price": 123,
  "categoryIds": [
    "00000000-0000-0000-0000-000000000000",
    "00000000-0000-0000-0000-000000000000",
    "00000000-0000-0000-0000-000000000000"
  ],
  "categories": [
    "category1",
    "category2",
    "category3"
  ]
}
```

##### Not Found

```http request
404 Not Found
```

```json
{
}
```

### Create Transaction

#### Create Transaction Request

```http request
POST /api/v1/users/{userId}/transactions
```

```json
{
}
```

#### Create Transaction Response

```http request
201 Created
```

```json
{
}
```

### Update Transaction

#### Update Transaction Request

```http request
PUT /api/v1/users/{userId}/transactions{transactionId}
```

```json
{
}
```

#### Update Transaction Response

##### Success

```http request
200 OK
```

```json
{
}
```

##### Not Found

```http request
404 Not Found
```

```json
{
}
```

### Delete Transaction

#### Delete Transaction Request

```http request
DELETE /api/v1/users/{userId}/transactions{transactionId}
```

```json
{
}
```

#### Delete Transaction Response

##### Success

```http request
204 No Content
```

```json
{
}
```

##### Not Found

```http request
404 Not Found
```

```json
{
}
```
