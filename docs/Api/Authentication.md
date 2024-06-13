# API docs

## Authentication

### Register

#### Register Request

```http request
POST /api/v1/users/register
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

##### Success

```http request
201 Created
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e..."
}
```

##### Email already exists

```http request
409 Conflict
```

```json
{
}
```

### Login

#### Login Request

```http request
POST /api/v1/users/login
```

```json
{
  "email": "email",
  "password": "password"
}
```

#### Login Response

##### Success

```http request
200 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e..."
}
```

##### Invalid email or password

```http request
401 Unauthorized
```

```json
{
}
```