using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentAdminPortalAPI.Core.Data;
using StudentAdminPortalAPI.Core.Infrastructures;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add connection to database
builder.Services.AddDbContext<StudentAdminContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Register Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("angularApplication", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PATCH", "PUT", "DELETE")
        .WithExposedHeaders("*");
    });
});

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
});

//app.UseCors(x => x
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader());

app.UseCors("angularApplication");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
