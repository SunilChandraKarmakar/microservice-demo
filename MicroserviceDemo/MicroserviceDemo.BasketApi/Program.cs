using MicroserviceDemo.BasketApi.GrpcServices;
using MicroserviceDemo.BasketApi.Repositories;
using MicroserviceDemo.DiscountgRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add redis connection string
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetConnectionString("RedisDb");
});

// Add gRPC client
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(option =>
{
    option.Address = new Uri("https://localhost:5004");
});
builder.Services.AddScoped<DiscountGrpcService>();

// Dependency injection
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();