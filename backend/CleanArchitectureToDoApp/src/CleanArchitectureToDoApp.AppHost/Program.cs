var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CleanArchitectureToDoApp_WebAPI>("cleanarchitecturetodoapp-webapi");

builder.Build().Run();
