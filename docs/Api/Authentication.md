# API docs

## Authentication

### Register

#### Register Request

```http request
POST /auth/register
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
  "id": "00000000-0000-0000-0000-000000000000",
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e..."
}
```

### Login

#### Login Request

```http request
POST /auth/login
```

```json
{
  "email": "email",
  "password": "password"
}
```

#### Login Response

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