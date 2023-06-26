using ELMS.Models;
using ELMS.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.Configure<ELMSDatabaseSettings>(
    builder.Configuration.GetSection("ELMSDatabase"));

builder.Services.AddSingleton<UserService>();

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
