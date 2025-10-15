using Microsoft.EntityFrameworkCore;
using Test_API.Data;  // ⭐ (for ApplicationContext)

var builder = WebApplication.CreateBuilder(args);

// ⭐ Register DbContext
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ⭐ Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ⭐ Swagger config
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// ⭐ Very important - this maps your controller routes!
app.MapControllers();

// Optional root route for browser check
app.MapGet("/", () => "✅ API is running successfully!");

app.Run();
