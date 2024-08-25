using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// Register ThesisServices with the desired lifetime
//builder.Services.AddScoped<ThesisServices>();

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

builder.Services.AddTransient<DocumentService>();
builder.Services.AddTransient<SupervisorService>();
builder.Services.AddTransient<ThesisServices>();
builder.Services.AddTransient<ReviewerService>();
builder.Services.AddTransient<ThesisSupervisorServices>();
builder.Services.AddTransient<ThesisReviewerServices>();
builder.Services.AddTransient<AuthorService>();
builder.Services.AddTransient<CatalogueService>();
builder.Services.AddTransient<CollectionService>();
builder.Services.AddTransient<ThemeService>();
builder.Services.AddTransient<EditorService>();
builder.Services.AddTransient<EmailService>();




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
