using Microsoft.AspNetCore.Cors.Infrastructure;
using TaskManagementService;
using TaskManagementService.Interface;
using TaskManagementService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

ConstantData.DbConstr = builder.Configuration.GetConnectionString("MsSqlConnection");


builder.Services.AddScoped<IUserManagement, UserManagementService>();
builder.Services.AddScoped<ICommanDbHander, CommanDbHander>();
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<ITaskManagement, TasksManagementService>();

var app = builder.Build();

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
