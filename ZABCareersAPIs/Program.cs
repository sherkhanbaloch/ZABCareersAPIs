using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using ZABCareersAPIs.Data;
using ZABCareersAPIs.Service.Implement;
using ZABCareersAPIs.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Resume Analaysis
builder.Services.AddSingleton<IArtificialIntelligence, ArtificialIntelligence>();
builder.Services.AddScoped<IResumeParser, ResumeParser>();
builder.Services.AddScoped<IResumeMatcher, ResumeMatcher>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Connection
var con = builder.Configuration.GetConnectionString("dbcs");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(con));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Scalar API reference mapping

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
