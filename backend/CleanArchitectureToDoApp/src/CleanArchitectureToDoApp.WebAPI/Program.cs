using CleanArchitectureToDoApp.Application;
using CleanArchitectureToDoApp.Infrastructure;
using CleanArchitectureToDoApp.WebAPI;
using CleanArchitectureToDoApp.WebAPI.Controller;
using CleanArchitectureToDoApp.WebAPI.Modules;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(); 

builder.Services.AddOpenApi();

builder.Services.AddControllers().AddOData(opt =>
        opt
        .Select()
        .Filter()
        .Count()
        .Expand()
        .OrderBy()
        .SetMaxTop(null)
        //.EnableQueryFeatures()
        .AddRouteComponents("odata", AppODataController.GetEdmModel())
//
);
builder.Services.AddRateLimiter(x=> 
x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.PermitLimit = 10;
    cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    cfg.Window = TimeSpan.FromSeconds(1);
}));

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapOpenApi();
app.MapScalarApiReference();

app.RegisterRoutes();

app.UseCors(x=> x
.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod());

app.UseExceptionHandler();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
