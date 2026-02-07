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
var employeeRoute = app.MapGroup("employees");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Serve the Swagger JSON
    app.UseSwaggerUI();      // Serve the Swagger UI page
}

app.UseHttpsRedirection();
employeeRoute.MapGet(string.Empty, () =>
{
    return Results.Ok(employees);
} );
employeeRoute.MapGet("{id:int}", (int id) =>
{
    var employee = employees.SingleOrDefault(e=>e.Id == id);
    if( employee == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(employee);
    // return employee;

});
employeeRoute.MapPost(string.Empty, (Employee employee) =>
{
    employee.Id = employees.Max(e=>e.Id)+1;
    employees.Add(employee);
    return Results.Created($"/employees/{employee.Id}", employee);
});
app.Run();
