using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation()
	.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
	.AddDataAnnotationsLocalization();

builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
	var supportedCultures = new List<CultureInfo>
				{
					new CultureInfo("fr"),
					new CultureInfo("en"),
					new CultureInfo("ar"),
					new CultureInfo("es"),
				};

	opt.DefaultRequestCulture = new RequestCulture("fr");
	opt.SupportedCultures = supportedCultures;
	opt.SupportedUICultures = supportedCultures;
});


builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromHours(5);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHttpClient<IEbookServiceApiClient, EbookServiceApiClient>((services, client) =>
{
	var config = services.GetRequiredService<IConfiguration>();
	var ebookServiceBackendUrl = config["AppSettings:EbookServiceApiUrl"];
	client.BaseAddress = new Uri(ebookServiceBackendUrl);
});

builder.Services.AddHttpClient<IThesisServiceApiClient, ThesisServiceApiClient>((services, client) =>
{
	var config = services.GetRequiredService<IConfiguration>();
	var thesisServiceBackendUrl = config["AppSettings:ThesisServiceApiUrl"];
	client.BaseAddress = new Uri(thesisServiceBackendUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.MapRazorPages();

app.Run();
