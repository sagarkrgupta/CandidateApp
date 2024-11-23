using CandidateApp.Dtos;
using CandidateApp.Business;
using CandidateApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using CandidateApp.DataAccess;
using CandidateApp.API.Infrastructures;

var builder = WebApplication.CreateBuilder(args);


// Extensions file config
builder.Services.AddControllers();  // Adds services for Web API controllers.


builder.Services.UseCustomBuilderServices(builder.Configuration);



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Add custom app builder build to the container.
app.UseCustomAppBuild();


// Configure endpoint mappings (for API controllers)
app.MapControllers(); // Map the API controllers


app.Run();
