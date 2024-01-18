## Finshark

C# ASP.NET case study project

## Requirements

- dotnet-core version 7.x.x
- docker >= 24.0.7

## How to run

#### Run SQL Server

```
docker compose up -d
```

#### Run db migrations

```
dotnet-ef database update
```

#### Run api server

```
dotnet run 
```

