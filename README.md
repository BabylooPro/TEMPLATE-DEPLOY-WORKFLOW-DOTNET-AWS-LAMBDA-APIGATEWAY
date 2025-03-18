# Deploy .NET 8 Minimal API to AWS Lambda/API Gateway – One Click (Almost) & Done!

This template automates the deployment of .NET 8 Minimal API projects to AWS Lambda with API Gateway integration with a single workflow file [.github/workflow/aws-deploy.yml](.github/workflow/aws-deploy.yml). It creates all necessary AWS resources including IAM roles, Lambda functions, and API Gateway configurations with proper security practices (not quite FBI level yet). The only manual step left is some clicky/moussing to integrate the IAM User in AWS Console, because this template is 98.89% automated! 🥳

> **INFO: Coming soon to the GitHub Actions Marketplace!**

## Features

- **Fully Parameterized Workflow**: Easy to customize for any .NET project
- **Secure AWS Configuration**: Creates roles with least privilege principles
- **Complete API Gateway Setup**: Configures API keys, usage plans, and CORS
- **Error Handling**: Preserves existing resources and handles duplicate creation attempts
- **GitHub Actions Integration**: Simple one-click deployment

## Workflow Steps

The workflow performs the following sequence of operations:

1. **GitHub OIDC Role Creation**: Creates or reuses a GitHub OIDC role for secure AWS authentication
2. **Testing** (Optional): Runs tests for the project if enabled
3. **Build & Package**: Compiles and packages the .NET application for Lambda deployment
4. **IAM Role Creation**: Creates or reuses a Lambda execution role with appropriate permissions
5. **Lambda Deployment**: Deploys the application as a Lambda function
6. **API Gateway Configuration**: Sets up API Gateway with throttling, usage plans, and API keys

## Prerequisites

1. **AWS IAM User**: Create an IAM user with the permissions listed in [.github/aws/iam-user-policy.json](.github/aws/iam-user-policy.json)
2. **GitHub Secrets**: Set up the following repository secrets:
   - `AWS_ACCESS_KEY_ID`: The IAM user's access key
   - `AWS_SECRET_ACCESS_KEY`: The IAM user's secret key
   - Any other secrets the application needs
3. **Properly Configured .NET Project**: Ensure the project uses the correct Lambda integration classes (see [Project Structure Requirements](#project-structure-requirements))

## Usage

### Basic Usage

1. Add this workflow file to the repository in [.github/workflow/aws-deploy.yml](.github/workflow/aws-deploy.yml)
2. Trigger the workflow manually from the GitHub Actions tab : `https://github.com/username/project/actions/workflows/aws-deploy.yml`

The workflow will run with default values and deploy the .NET application to AWS Lambda.

### Configuration Options

The workflow uses environment variables that can be customized:

| Variable                   |            Description            | Default                                                                 |
| -------------------------- | :-------------------------------: | ----------------------------------------------------------------------- |
| `DEFAULT_FUNCTION_NAME`    |       Lambda function name        | template-demo-deploy-minimal-api                                        |
| `DEFAULT_RESOURCE_PREFIX`  |     Prefix for AWS resources      | template-demo-deploy-minimal-api-                                       |
| `DEFAULT_PROJECT_PATH`     |     Path to the .NET project      | Demo/WeatherForecast                                                    |
| `DEFAULT_TESTS_PATH`       |     Path to the test project      | Demo/WeatherForecast.Tests                                              |
| `DEFAULT_LAMBDA_HANDLER`   |          Lambda handler           | WeatherForecast::WeatherForecast.LambdaEntryPoint::FunctionHandlerAsync |
| `DEFAULT_API_GATEWAY_TYPE` |  API Gateway type (REST or HTTP)  | REST                                                                    |
| `DEFAULT_MEMORY`           |   Lambda memory allocation (MB)   | 512                                                                     |
| `DEFAULT_TIMEOUT`          | Lambda function timeout (seconds) | 30                                                                      |
| `DEFAULT_THROTTLE_RATE`    |         API throttle rate         | 10                                                                      |
| `DEFAULT_THROTTLE_BURST`   |        API throttle burst         | 20                                                                      |
| `DEFAULT_QUOTA_LIMIT`      |          API quota limit          | 1000                                                                    |
| `DEFAULT_QUOTA_PERIOD`     |         API quota period          | DAY                                                                     |

### Project Structure Requirements

The .NET project should:

1. Be a Minimal API or ASP NET Core project configured for Lambda
2. Include the `Amazon.Lambda.AspNetCoreServer` package
3. Have a `LambdaEntryPoint` class that extends one of the following:
   - `APIGatewayProxyFunction` (for REST API) **[default structure in this workflow]**
   - `APIGatewayHttpApiV2ProxyFunction` (for HTTP API v2) **[causes issues in this workflow]**
   - `Other` **[untested]**

> **IMPORTANT**: The `LambdaEntryPoint` class must match the type of API Gateway being used. This workflow creates a REST API by default, so the class should extend `APIGatewayProxyFunction`. Using the wrong type will result in runtime errors.

## Demo Projects

The repository includes ready-to-deploy example projects in the `Demo` directory:

- **WeatherForecast**: A basic weather forecast API showcasing the default Lambda+API Gateway deployment. This is the default project referenced in the workflow configuration. It demonstrates:

  - Simple GET endpoint returning random weather data
  - Standard REST API Gateway integration
  - Minimal API architecture with default AWS Lambda handler
  - Modern .NET 8 structure with single Program.cs file (no Startup.cs)

- **Todo**: An alternative sample API for task management, demonstrating how to configure the workflow for different project structures. It showcases:
  - CRUD operations (Create, Read, Update, Delete)
  - Multiple API endpoints with different HTTP methods
  - More complex data model and business logic
  - How to adjust the workflow parameters for non-default projects
  - Traditional minimal ASP NET Core architecture with separate Program.cs and Startup.cs files

To deploy the WeatherForecast demo, simply run the workflow with default settings. To deploy the Todo demo, modify the `DEFAULT_PROJECT_PATH` parameter to `Demo/Todo` and adjust the `DEFAULT_LAMBDA_HANDLER` accordingly.

Both demos are configured with the proper Lambda entry points and can be deployed immediately using the provided workflow.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
