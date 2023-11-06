using System.Reflection;
using BookStore.Application;
using BookStore.Infrastructure;
using Microsoft.OpenApi.Models;
using BookStore.DataBase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Add Infrastructure layer services
builder.Services.AddInfrastructureServices();
// Add Application layer services
builder.Services.AddApplicationServices();
// Add DataBase layer services
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddRouting(options =>
{
     options.AppendTrailingSlash = true;
     options.LowercaseUrls = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo
     {
          Title = "Demo Jwt",
          Version = "v1"
     });

     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
          In = ParameterLocation.Header,
          Description = "Insert Jwt with Beard intro field",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
     });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement{
     {
          new OpenApiSecurityScheme
          {
               Reference = new OpenApiReference
               {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
               }
          },
          Array.Empty<string>()
     }});
     //Api Documentation
     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
     c.IncludeXmlComments(xmlPath);

});

//Add DataBase Context
builder.Services.AddDbContext<BookStoreDbContext>(options =>
{
     options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreDbConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
