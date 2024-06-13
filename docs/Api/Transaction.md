# API docs

## Transaction

### Query Transactions

#### Query Transactions Request

```http request
GET /api/v1/users/{userId}/transactions?from={from}&to={to}&description={description}&categories={categories}
```

```json
{
}
```

#### Query Transactions Response

```http request
200 OK
```

```json
[
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
