
using NaruuroApi.Model.Interface;
using NaruuroApi.Model.Repository;
using NaruuroApi.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddCors
// In your startup.cs file or wherever you configure your services


builder.Services.AddScoped<IBooking, BookingRepository>();
builder.Services.AddScoped<ICustomer, CustomerRepository>();

builder.Services.AddScoped<IRole, RoleRepo>();
builder.Services.AddScoped<IStaff, StaffRepo>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
//App.UseCors();
var app = builder.Build();




app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
