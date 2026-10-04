using ProductService.Application;
using ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application layer
builder.Services.AddApplication();

// Infrastructure layer
builder.Services.AddInfrastructure(
    builder.Configuration);

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    // Workaround: some Swagger UI builds reject the "3.0.4" patch version emitted
    // by Microsoft.OpenApi with "does not specify a valid version field".
    // Rewrite the served definition to the universally-recognized "3.0.1".
    app.Use(async (context, next) =>
    {
        if (context.Request.Path.Value?.EndsWith("swagger.json", StringComparison.OrdinalIgnoreCase) == true)
        {
            var originalBody = context.Response.Body;
            using var buffer = new MemoryStream();
            context.Response.Body = buffer;

            await next();

            buffer.Seek(0, SeekOrigin.Begin);
            var json = await new StreamReader(buffer).ReadToEndAsync();
            json = json.Replace("\"openapi\": \"3.0.4\"", "\"openapi\": \"3.0.1\"");

            context.Response.Body = originalBody;
            context.Response.ContentLength = null;
            await context.Response.WriteAsync(json);
            return;
        }

        await next();
    });

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();