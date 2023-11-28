using System.Configuration;
using AutoMapper;
using crudAPI.Mappings;
using crudAPI.Repository;
using crudAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile<MappingProfile>();
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

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

