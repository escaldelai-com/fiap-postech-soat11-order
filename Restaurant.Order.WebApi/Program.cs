using Restaurant.Order.Application;
using Restaurant.Order.Data;
using Restaurant.Order.ExternalServices;
using Restaurant.Order.Facade;
using Restaurant.Order.Presenter;
using Restaurant.Order.WebApi.Middleware;
using Restaurant.Order.WebApi;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services
    .AddPresenter()
    .AddData()
    .AddExternalServices()
    .AddApplication()
    .AddFacade()
    .AddAuthentication(builder.Configuration)
    .AddRestaurantAuthorization()
    .AddOpenApi();


// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandler();
app.UseErrorHandler();
app.MapControllers();


// Run the application
app.Run();
