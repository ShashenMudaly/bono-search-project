var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ... other service configurations ...

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// ... other middleware configurations ... 