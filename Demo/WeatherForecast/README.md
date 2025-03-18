# Weather Forecast Minimal API Solution

A .NET Core API built with simple Program.cs on Main approach and designed to run both locally and on AWS Lambda with API Gateway REST API.

## Deploying to AWS Lambda

This application is configured to run on AWS Lambda with API Gateway REST API. It uses the `APIGatewayProxyFunction` through the `LambdaEntryPoint` class that:

- Inherits from `APIGatewayProxyFunction`
- Uses `DefaultLambdaJsonSerializer` for request/response serialization
- Initializes the web host with the `Program` class as startup

## Endpoint

#### GET /weatherforecast

Returns a randomly generated list of 5 weather forecast items.

**Response:**

```json
[
  {
    "date": "2025-03-18",
    "temperatureC": 25,
    "temperatureF": 76,
    "summary": "Hot"
  },
  {
    "date": "2025-03-19",
    "temperatureC": 11,
    "temperatureF": 51,
    "summary": "Sweltering"
  },
  {
    "date": "2025-03-20",
    "temperatureC": 32,
    "temperatureF": 89,
    "summary": "Cool"
  },
  {
    "date": "2025-03-21",
    "temperatureC": 48,
    "temperatureF": 118,
    "summary": "Cool"
  },
  {
    "date": "2025-03-22",
    "temperatureC": 18,
    "temperatureF": 64,
    "summary": "Chilly"
  }
]
```
