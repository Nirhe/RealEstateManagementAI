using Microsoft.OpenApi.Models;

Console.WriteLine("Starting Real Estate Management API...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add logging
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();

    Console.WriteLine("Configuring services...");
    // Add services to the container.
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });
    
    builder.Services.AddControllers();

    // Add Swagger/OpenAPI generation for API exploration.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Real Estate Management API",
            Version = "v1",
            Description = "A starter backend for an AI‑enabled property management platform."
        });
    });

    Console.WriteLine("Building application...");
    
    // Configure Kestrel to use specific ports
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenLocalhost(5002); // HTTP
        serverOptions.ListenLocalhost(5003, listenOptions =>
        {
            listenOptions.UseHttps();
        });
    });
    
    // Ensure we're not using the default ports
    builder.WebHost.UseUrls("http://localhost:5002;https://localhost:5003");
    
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        Console.WriteLine("Configuring development environment...");
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => 
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate Management API v1");
        });
        
        // Enable CORS for development
        app.UseCors("AllowAll");
    }
    else
    {
        app.UseExceptionHandler("/error");
    }

    Console.WriteLine("Configuring middleware...");
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    Console.WriteLine("Application configured. Starting the server...");
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Application startup error: {ex}");
    throw;
}