using Dinner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System.Xml.XPath;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Dinner API",
        Description = "An ASP.NET Core Web API for managing What's For Dinner API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://localhost:7014/swagger/contact.html")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    //Reflection is used to build an XML file name matching that of the web API project.
    //The AppContext.BaseDirectory property is used to construct a path to the XML file.
    //For most features, namely method summaries and the descriptions of parameters and response codes, the use of an XML file is mandatory.
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
/*
builder.Services.AddSwaggerGen(options =>
{
 //   options.SwaggerDoc("v1", new OpenApiInfo { Title = "What's For Dinner", Version = "v1" });
  //  options.IncludeXmlComments();

    //options.IncludeXmlComments(XmlCommentsFilePath);
});*/
builder.Services.AddDbContext<AppDbContext>(Options =>
{
    Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "What's for Dinner V1");
    });
}

app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/swagger-ui/custom.css");
});

app.UseHttpsRedirection();



app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
