using CreateAccount.AggregateRoot.Validation;
using CreateAccount.DTO.DTOs;
using CreateAccount.Handler.Abstraction;
using CreateAccount.Handler.Service;
using CreateAccount.Repository.Repository;
using CreateAccount.Repository.Repository.Abstraction;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

    //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CheckNamesRequestDTOValidator>());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGenericRepository, GenericRepository>();

builder.Services.AddScoped<CheckNamesHandler>();
builder.Services.AddScoped<ICheckNamesHandler, CheckNamesHandler>();
builder.Services.AddScoped<IValidator<CheckNamesRequestDTO>, CheckNamesRequestDTOValidator>();

builder.Services.AddScoped<IValidator<LocationRequestDTO>, LocationRequestDTOValidator>();
builder.Services.AddScoped<ILocationHandler, LocationHandler>();

builder.Services.AddScoped<IIndustryTypeHandler,IndustryTypeHandler>();
builder.Services.AddScoped<IValidator<IndustryTypeRequestDTO>, IndustryTypeRequestDTOValidator>();

builder.Services.AddScoped<IRLNoHandler, RLNoHandler>();
builder.Services.AddScoped<IValidator<RLNoRequestDTO>, RLDTOValidator>();



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
app.UseAuthorization();
app.MapControllers();

app.Run();
