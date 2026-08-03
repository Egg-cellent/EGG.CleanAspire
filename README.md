\# Clean Architecture + Aspire Template



.NET 10 | C# 14 | Aspire 13 | EF Core 10 | xUnit v3



\## Tech Stack



| Layer | Technology |

|-------|-----------|

| \*\*Architecture\*\* | Clean Architecture (Domain, Application, Infrastructure, Api) |

| \*\*Runtime\*\* | .NET 10 / C# 14 |

| \*\*API\*\* | Minimal APIs with TypedResults |

| \*\*CQRS\*\* | Mediator |

| \*\*Validation\*\* | FluentValidation 12 + Result pattern |

| \*\*Errors\*\* | ProblemDetails + global exception handler |

| \*\*Database\*\* | EF Core 10 + PostgreSQL |

| \*\*Caching\*\* | Microsoft HybridCache (L1 in-memory + L2 Redis) |

| \*\*Auth\*\* | ASP.NET Identity + JWT with refresh tokens |

| \*\*API Docs\*\* | Scalar (modern OpenAPI UI) |

| \*\*Logging\*\* | Serilog 10 structured logging |

| \*\*Observability\*\* | .NET Aspire 13 + OpenTelemetry (traces, metrics, logs) |

| \*\*Testing\*\* | xUnit v3 + FluentAssertions + NSubstitute + NetArchTest |

| \*\*Solution\*\* | `.slnx` format + Central Package Management |



\## Architecture



```

┌──────────────────────────────────────────────────┐

│                    Api Layer                     │

│         Endpoints, Program.cs, Scalar            │

└──────────────────┬───────────────────────────────┘

&#x20;                  │ depends on

┌──────────────────▼───────────────────────────────┐

│              Infrastructure Layer                │

│     EF Core, Identity, JWT, HybridCache          │

└──────────────────┬───────────────────────────────┘

&#x20;                  │ depends on

┌──────────────────▼───────────────────────────────┐

│              Application Layer                   │

│      CQRS Handlers, Validators, DTOs             │

└──────────────────┬───────────────────────────────┘

&#x20;                  │ depends on

┌──────────────────▼───────────────────────────────┐

│                Domain Layer                      │

│      Entities, Result, Repositories (interfaces) │

└──────────────────────────────────────────────────┘

```



\## Getting Started



\### Prerequisites



\- \[.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

\- \[Docker Desktop](https://www.docker.com/products/docker-desktop/) (for Aspire containers)



\### Run with Aspire (recommended)



```bash

cd src/EGG.CleanAspire.AppHost

dotnet run

```



This starts everything:

\- \*\*PostgreSQL\*\* database with pgAdmin

\- \*\*Redis\*\* cache with RedisInsight

\- \*\*API\*\* with auto-migration and seed data

\- \*\*Aspire Dashboard\*\* for OpenTelemetry (traces, metrics, logs)





\### Run without Aspire



```bash

\# Start dependencies

docker compose up -d



\# Run the API

cd src/EGG.CleanAspire.Api

dotnet run

```



\## Trivia



\### Create Migration



```bash

dotnet ef migrations add InitialCreate 

\--project Infrastructure\\EGG.CleanArchitecture.Infrastructure.csproj 

\--startup-project Api\\EGG.CleanArchitecture.Api.csproj 

\--output-dir Persistence/Migrations

```



\### Create User Secret



```bash

\# Postgresql

dotnet user-secrets set "Aspire:Resources:postgres:User" "postgres" 

\--project src/AppHost/EGG.CleanAspire.AppHost.csproj



dotnet user-secrets set "Aspire:Resources:postgres:Password" "12345678" --project src/AppHost/EGG.CleanAspire.AppHost.csproj



\# Redis

dotnet user-secrets set "Aspire:Resources:egg-cache:Password" "redispass" --project src/AppHost/EGG.CleanAspire.AppHost.csproj

```



\## License



MIT License.

