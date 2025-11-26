using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleContactForm.Abstractions.Interfaces;
using SimpleContactForm.DataAccess;
using SimpleContactForm.Service;
using SimpleContactForm.Utils.Utils;

var builder = WebApplication.CreateBuilder(args);

var connection = new SqliteConnection(builder.Configuration.GetConnectionString("SqlLite"));
connection.Open();
builder.Services.AddDbContext<ContactFormDbContext>(x =>
    x.UseSqlite(connection));

builder.Services.AddScoped<ExceptionHandleMiddleware>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseMiddleware<ExceptionHandleMiddleware>();
app.Run();