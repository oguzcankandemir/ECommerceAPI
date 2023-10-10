
using ECommerce.Application.Abstractions;
using ECommerce.Persistence.Concretes;

var builder = WebApplication.CreateBuilder(args);
// for Extension static method 
//builder.Services.AddPersistenceServices();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
