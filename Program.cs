using InterviewTest;
using InterviewTest.Models;
using InterviewTest.Data;
using Microsoft.EntityFrameworkCore;
using InterviewTest.Factory;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Tambahkan koneksi ke PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 🔹 Tambahkan Controller & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IPostFactory, PostFactory>(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "InterviewTest API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 🔹 Pastikan Controller terdaftar di route
app.MapControllers();

app.Run();
