using BwssbRestfulAPI.DBServices;
using BwssbRestfulAPI.Middleware;
using BwssbRestfulAPI.Repository;
using BwssbRestfulAPI.Repository.AxisKioskRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//--- APIServicesClass

builder.Services.AddScoped<ChallanRepository>();
builder.Services.AddScoped<ReceiptRepository>();

//--- DBServices
builder.Services.AddScoped<IDBServices, DBServiceClass>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add the custom error handling middleware
app.UseErrorHandlingMiddleware();

//Configure the HTTP request pipeline.
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
