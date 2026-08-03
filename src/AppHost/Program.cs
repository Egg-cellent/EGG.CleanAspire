var builder = DistributedApplication.CreateBuilder(args);

var pgUser = builder.AddParameter("postgresql-username", secret: false);
var pgPassword = builder.AddParameter("postgresql-password", secret: true);
var redisPassword = builder.AddParameter("redis-password", secret: true);

var postgres = builder.AddPostgres("postgres", userName: pgUser, password: pgPassword, port: 5432)
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("postgres-data");

var database = postgres.AddDatabase("egg-db");

var redis = builder.AddRedis("egg-cache", password: redisPassword, port: 6379)
    .WithRedisInsight()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.EGG_CleanAspire_Api>("api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(redis)
    .WaitFor(redis)
    .WithExternalHttpEndpoints();

builder.Build().Run();
