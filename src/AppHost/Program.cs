var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var database = postgres.AddDatabase("egg-db");

var redis = builder.AddRedis("egg-cache")
    .WithRedisInsight()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.EGG_CleanAspire_Api>("api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(redis)
    .WaitFor(redis)
    .WithExternalHttpEndpoints();

builder.Build().Run();
