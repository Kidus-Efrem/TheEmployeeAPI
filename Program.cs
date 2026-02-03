var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer(); // Needed for Swagger
builder.Services.AddSwaggerGen();           // <-- This was missing

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Serve the Swagger JSON
    app.UseSwaggerUI();      // Serve the Swagger UI page
}

app.UseHttpsRedirection();
app.MapGet("/employees", );

app.Run();
