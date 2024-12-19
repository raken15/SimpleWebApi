var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Registers the controllers with the dependency injection container.
                                   // Adds support for MVC
                                   // Allows ASP.NET Core to know how to process incoming requests for controllers
                                   // And allows me to create API endpoints.

// Add Swagger services
builder.Services.AddEndpointsApiExplorer(); // Required for the endpoint API explorer, 
                                            // for swagger to recognize endpoints and actions in controllers to document
builder.Services.AddSwaggerGen(); // Registers Swagger generator to Services
                                  // ASP.NET Core to generate the Swagger documentation, which will describe your API's routes, methods, parameters, and more.

var app = builder.Build(); // The DI container is set up, and ASP.NET Core is ready to start the HTTP request pipeline

// var url = app.Urls.FirstOrDefault() ?? "URL not found"; // Gets the first URL from the list
// app.Logger.LogInformation("Application is running at: {Url}", url);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // This will generate Swagger JSON
    app.UseSwaggerUI(); // This will serve the Swagger UI at /swagger
}

app.UseHttpsRedirection(); // Enforces HTTPS
app.MapControllers(); // Tell ASP.NET Core to map HTTP requests to the appropriate controller actions

app.Run(); // Starts the web application