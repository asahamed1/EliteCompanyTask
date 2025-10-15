using EliteCompanyTask.Domain.Entities;
using EliteCompanyTask.Domain.IRepositories;
using EliteTask.Application.IServices;
using EliteTask.Application.Mappers;
using EliteTask.Application.Services;
using EliteTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile));
builder.Services.AddScoped(typeof(IAccountBalanceHistoryReposiory), typeof(AccountBalanceHistoryReposiory));
builder.Services.AddScoped(typeof(IDapperService), typeof(DapperService));
builder.Services.AddScoped(typeof(IExcelExportService), typeof(ExcelExportService));
builder.Services.AddScoped(typeof(IPdfExportService), typeof(PdfExportService));
builder.Services.AddScoped(typeof(IBalanceService), typeof(BalanceService));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .AllowAnyOrigin()    // or .WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowFrontend");
app.UseStaticFiles(); // ? This enables serving files from wwwroot

app.MapGet("/", async context =>
{
    context.Response.Redirect("/index.html");
});

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
