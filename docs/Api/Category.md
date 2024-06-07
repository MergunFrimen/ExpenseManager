# API docs

## Category

### Create Category

#### Create Category Request

```http request
POST /categories
```

```json
{
  "name": "Food"
}
```

#### Create Category Response

```http request
201 Created
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Food"
}
```

### Update Category

#### Update Category Request

```http request
PUT /categories
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "Food"
}
```

#### Update Category Response

```http request
200 OK
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Food"
}
```

### Delete Category

#### Delete Category Request

```http request
DELETE /categories
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000"
}
```

#### Delete Category Response

```http request
200 OK
```

```json
{
}
```

### List Categories

#### List Categories Request

```http request
GET /categories
```

```json
{
}
```

#### List Categories Response

```http request
200 OK
```

```json
{
  "categories": [
    {
      "id": "00000000-0000-0000-0000-000000000000",
      "name": "Food"
    },
    {
      "id": "00000000-0000-0000-0000-000000000001",
      "name": "Transport"
    }
  ]
}
```
