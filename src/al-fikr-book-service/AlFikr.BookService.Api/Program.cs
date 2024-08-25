using AlFikr.BookService.Business;
using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDbContext<AlFikrContext>((services, options) =>
{
    var config = services.GetRequiredService<IConfiguration>();
    var conn = config.GetConnectionString("AlFikr");
    options.UseMySql(conn, ServerVersion.AutoDetect(conn))
    .EnableDetailedErrors();
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddTransient<AuthorService>();
builder.Services.AddTransient<DocumentService>();
builder.Services.AddTransient<TranslationGroupService>();
builder.Services.AddTransient<ThemeService>();
builder.Services.AddTransient<SubThemeService>();
builder.Services.AddTransient<CollectionService>();
builder.Services.AddTransient<CatalogueService>();
builder.Services.AddTransient<EbookService>();
builder.Services.AddTransient<ChapterService>();
builder.Services.AddTransient<DocumentFilesService>();
builder.Services.AddTransient<VolumeGroupService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
