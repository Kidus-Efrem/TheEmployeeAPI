var builder = WebApplication.CreateBuilder(args);
var employees = new List<Employee>(){
        new() { Id = 1, FirstName = "John", LastName = "Doe"},
        new() {Id = 2 , FirstName = "Jane", LastName = "Doe"}
};
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
app.MapGet("/employees", () =>
{
    return Results.Ok(employees);
} );
app.MapGet("/employees/{id:int}", (int id) =>
{
    var employee = employees.SingleOrDefault(e=>e.Id == id);
    if( employee == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(employee);
    // return employee;

});
app.Run();
