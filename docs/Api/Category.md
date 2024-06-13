# API docs

## Category

### Query Categories

#### Query Categories Request

```http request
GET /api/v1/users/{userId}/categories?name={name}
```

```json
{
}
```

#### Query Categories Response

```http request
200 OK
```

```json
[
]
```

### Get Category

#### Get Category Request

```http request
GET /api/v1/users/{userId}/categories/{categoryId}
```

```json
{
}
```

#### Get Category Response

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

### Create Category

#### Create Category Request

```http request
POST /api/v1/users/{userId}/categories
```

```json
{
}
```

#### Create Category Response

```http request
201 Created
```

```json
{
}
```

### Update Category

#### Update Category Request

```http request
PUT /api/v1/users/{userId}/categories{categoryId}
```

```json
{
}
```

#### Update Category Response

##### Success

```http request
200 OK
```

```json
{
}
```

##### Not Found

-the endpoint is valid but the resource itself does not exist

```http request
404 Not Found
```

```json
{
}
```

### Delete Category

#### Delete Category Request

```http request
DELETE /api/v1/users/{userId}/categories{categoryId}
```

```json
{
}
```

#### Delete Category Response

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
