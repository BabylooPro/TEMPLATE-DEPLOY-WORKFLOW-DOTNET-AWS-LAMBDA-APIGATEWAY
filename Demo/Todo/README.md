# Demo Todo Minimal API Solution

A .NET Core API built with traditional Startup.cs approach and designed to run both locally and on AWS Lambda with API Gateway REST API.

## Deploying to AWS Lambda

This application is configured to run on AWS Lambda with API Gateway REST API. It uses the `APIGatewayProxyFunction` through the `LambdaEntryPoint` class that:

- Inherits from `APIGatewayProxyFunction`
- Uses `DefaultLambdaJsonSerializer` for request/response serialization (set with `[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]`)
- Initializes the web host with the `Startup` class using `builder.UseStartup<Startup>()`

Note: There is a known issue with NullReferenceException in MarshallRequest method when handling API Gateway requests with "APIGatewayHttpApiV2ProxyFunction".

## Endpoints

#### GET /todo

Returns all todo items.

**Response:**

```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "title": "Complete project documentation",
    "description": "Write comprehensive API documentation for the Todo app",
    "isCompleted": false,
    "createdAt": "2023-04-01T10:00:00Z",
    "completedAt": null
  },
  {
    "id": "5c9c8f63-b2e4-4c0d-8141-7b4e0e37f34b",
    "title": "Test Lambda deployment",
    "description": "Deploy and test the API on AWS Lambda",
    "isCompleted": true,
    "createdAt": "2023-04-01T11:30:00Z",
    "completedAt": "2023-04-02T09:15:00Z"
  }
]
```

#### GET /todo/{id}

Returns a specific todo item by ID.

**Parameters:**

- `id` (GUID) - The ID of the todo item

**Response:**

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Complete project documentation",
  "description": "Write comprehensive API documentation for the Todo app",
  "isCompleted": false,
  "createdAt": "2023-04-01T10:00:00Z",
  "completedAt": null
}
```

**Status Codes:**

- 200 OK - Success
- 404 Not Found - Todo item not found

#### POST /todo

Creates a new todo item.

**Request Body:**

```json
{
  "title": "Learn AWS Lambda",
  "description": "Study AWS Lambda and API Gateway integration",
  "isCompleted": false
}
```

**Response:**

```json
{
  "id": "8f7e6d5c-4b3a-2c1d-0e9f-8a7b6c5d4e3f",
  "title": "Learn AWS Lambda",
  "description": "Study AWS Lambda and API Gateway integration",
  "isCompleted": false,
  "createdAt": "2023-04-03T14:25:32Z",
  "completedAt": null
}
```

**Status Codes:**

- 201 Created - Todo successfully created

#### PUT /todo/{id}

Updates an existing todo item.

**Parameters:**

- `id` (GUID) - The ID of the todo item to update

**Request Body:**

```json
{
  "title": "Learn AWS Lambda and API Gateway",
  "description": "Study AWS Lambda and API Gateway integration in depth",
  "isCompleted": true
}
```

**Status Codes:**

- 204 No Content - Todo successfully updated
- 404 Not Found - Todo item not found

#### DELETE /todo/{id}

Deletes a todo item.

**Parameters:**

- `id` (GUID) - The ID of the todo item to delete

**Status Codes:**

- 204 No Content - Todo successfully deleted
- 404 Not Found - Todo item not found

#### PATCH /todo/{id}/toggle

Toggles the completion status of a todo item.

**Parameters:**

- `id` (GUID) - The ID of the todo item to toggle

**Status Codes:**

- 204 No Content - Todo completion status successfully toggled
- 404 Not Found - Todo item not found
