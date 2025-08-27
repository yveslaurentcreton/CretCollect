var builder = DistributedApplication.CreateBuilder(args);

var keycloakAdminUserName = builder.AddParameter("keycloak-admin-username");
var keycloakAdminPassword = builder.AddParameter("keycloak-admin-password");

var keycloak = builder.AddKeycloak("cretcollect-keycloak", 26000, keycloakAdminUserName, keycloakAdminPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithRealmImport("realms")
    .WithDataVolume();

var database = builder
    .AddPostgres("cretcollect-db-server")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("cretcollect-db");

var webapi = builder.AddProject<Projects.CretCollect_WebApi>("cretcollect-webapi")
    .WithExternalHttpEndpoints()
    .WithReference(keycloak)
    .WithReference(database);

builder.AddProject<Projects.CretCollect_App_Server>("cretcollect-app")
    .WithExternalHttpEndpoints()
    .WithReference(webapi)
    .WithReference(keycloak);

builder.Build().Run();
