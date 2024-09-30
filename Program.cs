using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EmpresaApi.Data;
using EmpresaApi;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmpresaApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EmpresaApiContext") ?? throw new InvalidOperationException("Connection string 'EmpresaApiContext' not found.")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEmpresaEndpoints();

app.MapEnderecoEndpoints();

app.MapFuncionarioEndpoints();

app.Run();

