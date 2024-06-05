# API docs

## Authentication

### Register

#### Register Request

```http request
POST /api/register
```

```json
{
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "password": "password"
}
```

#### Register Response

```http request
201 Created
```

```json
{
  "id": "48f85641-9964-413b-9911-3d3529875671",
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e..."
}
```

### Login

#### Login Request

```http request
POST /api/login
```

```json
{
  "email": "username",
  "password": "password"
}
```

#### Login Response

```http request
200 OK
```

```json
{
  "id": "48f85641-9964-413b-9911-3d3529875671",
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e..."
}
```