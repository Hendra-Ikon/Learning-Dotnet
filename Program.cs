using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Menambahkan DbContext dengan konfigurasi koneksi ke PostgreSQL
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EmployeeConnection")));

// Menambahkan layanan controller
builder.Services.AddControllers();

// Menambahkan OpenAPI/Swagger untuk dokumentasi API (jika perlu)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Mengonfigurasi middleware untuk routing API dan Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Menentukan endpoint untuk controllers
app.MapControllers();

app.Run();
