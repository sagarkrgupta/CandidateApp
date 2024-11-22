using CandidateApp.Dtos;
using CandidateApp.Business;
using CandidateApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapperServices();
builder.Services.RegisterScopedServices();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
