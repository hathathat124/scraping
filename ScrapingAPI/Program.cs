using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scraping.Interfaces;
using Scraping.Interfaces.IProcess;
using Scraping.Process;
using Scraping.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMakroScrapingProcess, MakroScrapingProcess>();
builder.Services.AddScoped<IShoppeeScrapingProcess, ShoppeeScrapingProcess>();
builder.Services.AddScoped<IScrapingService, ScrapingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scraping.API v1");
});
//}

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<string>();


app.UseRouting();
app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});


app.Run();
