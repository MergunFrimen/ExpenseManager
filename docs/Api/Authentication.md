# API docs

## Authentication

### Register

#### Register Request

```http
POST /api/register
```

```json
{
    "username": "username",
    "password": "password"
}
```

#### Register Response

```http
201 Created
```

```json
{
  "id": "48f85641-9964-413b-9911-3d3529875671",
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJFeHBlbnNlTWFuYWdlci5Eb21haW4uVXNlcnMuVmFsdWVPYmplY3RzLlVzZXJJZCIsImdpdmVuX25hbWUiOiJEb21pbmlrIiwiZmFtaWx5X25hbWUiOiJUaWNoeSIsImp0aSI6ImZlYzk5MDlmLWYwZGQtNDQ4YS04OGM0LWFlZmU0MDFlMzRlMCIsImlzcyI6IkV4cGVuc2VNYW5hZ2VyIiwiYXVkIjoiRXhwZW5zZU1hbmFnZXIiLCJleHAiOjE3MTc1OTE2NTl9.sc-Ve2_zKu_yNMBWXrjyIvODkM61w0CIMEbVR8Jy208"
}
```

### Login

#### Login Request

```http
POST /api/login
```

```json
{
    "username": "username",
    "password": "password"
}
```

#### Login Response

```http
200 OK
```

```json
{
  "id": "48f85641-9964-413b-9911-3d3529875671",
  "firstName": "Dominik",
  "lastName": "Tichy",
  "email": "dominik@tichy.cz",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJFeHBlbnNlTWFuYWdlci5Eb21haW4uVXNlcnMuVmFsdWVPYmplY3RzLlVzZXJJZCIsImdpdmVuX25hbWUiOiJEb21pbmlrIiwiZmFtaWx5X25hbWUiOiJUaWNoeSIsImp0aSI6Ijg5OWQ1ZjhhLTViM2MtNDNjYi1iMTQ0LWMxNDkwZGY1YzlmNSIsImlzcyI6IkV4cGVuc2VNYW5hZ2VyIiwiYXVkIjoiRXhwZW5zZU1hbmFnZXIiLCJleHAiOjE3MTc1OTE3NDd9.yCwo25YnsGR9cHVv9eMMUPZeNLVRvkcPkqWuVvKssIc" 
}